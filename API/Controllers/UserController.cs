using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.ListAll();
            return Ok(users);

        }
        [HttpGet("")]
        public IActionResult CurrentUserData()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _unitOfWork.UserRepository.GetByPredicate(x => x.UserId == id, "Memories.UsersTaggedOnMemory.User");

            if (user != null)
            {
                return Ok(_mapper.Map<Users, UserDto>(user));
            }
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("messages/{id}")]
        public IActionResult GetUserMessagesWithAnotherUser([FromRoute] int id)
        {
            var currUserId = Int32.Parse(User.FindFirst("MyId").Value);
            var messages = _unitOfWork.MessagingRepository.GetByPredicateList(x => ((x.FromId == id || x.ToId == id) && (x.ToId == currUserId || x.FromId == currUserId)));

            if (messages != null)
            {
                return Ok(_mapper.Map<List<Messages>, List<MessagesDto>>(messages));
            }
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("status/{id}")]
        public IActionResult GetUserRelationshipStatus([FromRoute] int id)
        {
            var currUserId = Int32.Parse(User.FindFirst("MyId").Value);

            var status = _unitOfWork.FriendsRepository.GetRelationshipStatus(currUserId, id);

            return Ok(status);
        }

        [HttpGet("details/{id}")]
        public IActionResult GetUserDetails([FromRoute] int id)
        {
            var user = _unitOfWork.UserRepository.GetByPredicate(x => x.UserId == id, "UsersRelationshipFirstUser.SecondUser", "UsersRelationshipSecondUser");

            return Ok(user);
        }
    }
}
