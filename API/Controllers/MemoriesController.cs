using API.Errors;
using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using System;
using System.Collections;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public MemoriesController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult CreateMemory(MemoryDto memory)
        {
            var entity = _unitOfWork.MemoryRepository.Insert(_mapper.Map<MemoryDto, Memories>(memory));
            _unitOfWork.SaveChanges();
            var memoryToReturn = _mapper.Map<Memories, MemoryDto>(entity);
            foreach (var userTagged in memoryToReturn.UsersTaggedOnMemory)
            {
                var user = _unitOfWork.UserRepository.GetByID(userTagged.UserId);
                _emailService.Send("isthisevenimportant@email.com", user.Email, "This is the subjectttt", "<h1>Sretan Bozic</h1><p>Dobro tis bog</p>");
            }
            return Ok(memoryToReturn);
        }

        [Authorize]
        [HttpGet("")]
        public IEnumerable MemoriesFeed()
        {
            return _unitOfWork.MemoryRepository.ListAll();
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public string GetMemoriesRelatedToUser([FromRoute] int id)
        {
            // Call query factory or repository or something
            // var title = _context.Memories.FirstOrDefault(x => x.MemoryId == id).Title;
            return "single memory i guess with id" + id + "endlineheh";
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetMemoryById([FromRoute] int id)
        {
            var memory = _unitOfWork.MemoryRepository.GetByID(id);
            if(memory != null)
            {
                return Ok(_mapper.Map<Memories, MemoryDto>(memory));
            }
            return NotFound(new ApiResponse(404));
        }

    }
}
