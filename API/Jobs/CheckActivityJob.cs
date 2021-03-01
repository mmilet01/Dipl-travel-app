using API.Hubs;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Jobs
{
    public class CheckActivityJob : ICheckActivityJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationHub _notificationHub;

        public CheckActivityJob(IUnitOfWork unitOfWork, INotificationHub notificationHub)
        {
            _unitOfWork = unitOfWork;
            _notificationHub = notificationHub;

        }

        public void CheckActivity()
        {
            Console.WriteLine("JOB STARTED");
            Console.WriteLine("JOB STARTED");
            var users = _unitOfWork.UserRepository.ListAllWithInclude("UsersRelationshipFirstUser.SecondUser", "UsersRelationshipFirstUser");
            foreach (var user in users)
            {
                TimeSpan ts = DateTime.Now - user.LastActivityTimeStamp;
                if (ts.TotalMinutes > 2 && user.IsActive)
                {
                    user.IsActive = false;
                    foreach(var friend in user.UsersRelationshipFirstUser)
                    {
                        if(friend.RelationshipStatus == (int)UserRelationshipStatusEnum.Friends)
                        {
                            _notificationHub.SendNotification(friend.SecondUser.Email, NotificationType.UserStatusChange);
                            //signal r notify this -> friend.SecondUser.Email with message, user is offline/online
                        }
                    }
                }
                else if(ts.TotalMinutes < 2 && !user.IsActive)
                {
                    user.IsActive = true;
                    foreach (var friend in user.UsersRelationshipFirstUser)
                    {
                        if (friend.RelationshipStatus == (int)UserRelationshipStatusEnum.Friends)
                        {
                            _notificationHub.SendNotification(friend.SecondUser.Email, NotificationType.UserStatusChange);
                            //signal r notify this -> friend.SecondUser.Email
                        }
                    }
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}
