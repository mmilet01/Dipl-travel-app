using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Hubs;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly INotificationHub _hubContext;

        private readonly IUnitOfWork _unitOfWork;
        public FriendsController(IUnitOfWork unitOfWork, INotificationHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;

        } 

        [Authorize]
        [HttpPost("sent/{id}")]
        public async Task<IActionResult> SendFriendRequest([FromRoute] int id)
        {
            _unitOfWork.FriendsRepository.FriendRequestSent(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, id);

            var RecievedFriendRequestUser = _unitOfWork.UserRepository.GetByID(id);
            // unitOfWork.NotificationRepository.Add(Notification) - make dto and everything related
            // send to frontend and if recieved send back to server and change read inside DB
            // make hangfire can do this?
            // call notificationRepo? notification Service ? sendNotification
            await _hubContext.SendNotification(RecievedFriendRequestUser.Email, NotificationType.FriendRequestRecieved);
        //    await _hubContext.Clients.User(RecievedFriendRequestUser.Email).SendAsync("NotificationRecieved", User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value,NotificationType.FriendRequestRecieved);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpPost("respond/{id}")]
        public IActionResult FriendRequestResponded([FromRoute] int id, [FromBody] int response)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            _unitOfWork.FriendsRepository.FriendRequestResponded(email, id, (FriendRequestResponse)response);
            _unitOfWork.SaveChanges();

            return Ok();
        }
    }
}
