using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Errors;
using AutoMapper;
using Hangfire;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDataProtector _protector;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _backgroundJobs;

        public AccountController(
            ILogger<AccountController> logger,
            IDataProtectionProvider provider,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IMapper mapper,
            IBackgroundJobService backgroundJobs
            )
        {
            _logger = logger;
            _protector = provider.CreateProtector("passwordProtection");
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
            _backgroundJobs = backgroundJobs;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            var httpContext = HttpContext;
            if(!httpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized(returnUrl);
            }
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel data, string returnUrl = "/")
        {
            var user = _unitOfWork.UserRepository.GetByEmail(data.Email);
            if (user != null)
            {
                return BadRequest(new ApiResponse(400, "Email already in use"));
            }
            // Validate pw and return if validation doesn't pass, introduce try catch block for better error handling
            var hashedPw = _protector.Protect(data.Password);
            var newUser = new Users {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                CreatedOn = DateTime.UtcNow,
                PasswordField = hashedPw,
            };
            


            _unitOfWork.UserRepository.Insert(newUser);
            _unitOfWork.SaveChanges();
            var userInDB = _unitOfWork.UserRepository.GetByEmail(data.Email);

            var claims = new List<Claim>
            {
                new Claim("MyId", userInDB.UserId.ToString()),
                new Claim(ClaimTypes.Name, data.FirstName),
                new Claim(ClaimTypes.NameIdentifier, data.Email),
                new Claim("FullName", data.FirstName + data.LastName),
                new Claim(ClaimTypes.Role, "User"),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "mmiletaAuthCookieScheme");
            var principal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new AuthenticationProperties
               {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                RedirectUri = "http://localhost:3000"
             };

            await HttpContext.SignInAsync(
                "mmiletaAuthCookieScheme",
                principal,
                authProperties
                );

            _backgroundJobs.FireAndForget(() => _emailService.Send("isthisevenimportant@email.com", "testingdiplomski@gmail.com", "This is the subject", "<h1>Sretan Bozic</h1><p>Dobro tis bog</p>"));


            return Ok(_mapper.Map<Users, UserData>(newUser));

        }

       [HttpPost("login")]
       public async Task<IActionResult> LoginAsync([FromBody] LoginModel data, string returnUrl = "/")
       {
            var user = _unitOfWork.UserRepository.GetByEmail(data.Email);
            if(user == null)
            {
                return BadRequest(new ApiResponse(400, "Email not found"));
            }
            if(data.Password != _protector.Unprotect(user.PasswordField))
            {
                return BadRequest(new ApiResponse(400, "Email and password do not match"));
            }
            var claims = new List<Claim>
            {
                new Claim("MyId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, data.FirstName),
                new Claim(ClaimTypes.NameIdentifier, data.Email),
                new Claim("FullName", data.FirstName + data.LastName),
                new Claim(ClaimTypes.Role, "User"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, "mmiletaAuthCookieScheme");
            var principal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                RedirectUri = "http://localhost:3000"
            };

            await HttpContext.SignInAsync(
                "mmiletaAuthCookieScheme",
                principal,
                authProperties
                );

            return Ok(user);
       }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
             await HttpContext.SignOutAsync("mmiletaAuthCookieScheme");
             var httpctx = HttpContext;
             return Ok();
        }
    }
}
