using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Hubs
{
    [Authorize]
    public class MessagingHub : Hub
    {
        private Dictionary<string, string> _connections =
               new Dictionary<string, string>();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessagingHub(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SendPrivateMessage(string userEmail, string message)
        {
            var RecivingMessageUser = _unitOfWork.UserRepository.GetByEmail(userEmail);

            var currUserEmail = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sender = _unitOfWork.UserRepository.GetByEmail(currUserEmail);

            var newMessage = new MessagesDto
            {
                FromId = sender.UserId,
                ToId = RecivingMessageUser.UserId,
                MessageBody = message,
                SentAt = DateTime.UtcNow,
            };

            // send message where its supossed to go and to the guy that sent it ----> actually no - more chats, where does that message go?
            // send a sender of a message? -> Context.User -> if sender + user taht message needs to goto

            // NEED TO USE CONNECTION ID
            //   var connectionId = "";
            //  bool v = _connections.TryGetValue(user.Email, out connectionId);

            // await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            await Clients.Group(userEmail).SendAsync("ReceiveMessage", message);

            _unitOfWork.MessagingRepository.Insert(_mapper.Map<MessagesDto, Messages>(newMessage));
            _unitOfWork.SaveChanges();

        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync(message);
        }


        public override Task OnConnectedAsync()
        {
            // lock (_connections)
            //  {
            //      _connections.Add("asd", Context.ConnectionId);
            // 
            var groupName = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
          //  Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
      //      _connections.Remove(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return base.OnDisconnectedAsync(ex);
        }
    }
}
