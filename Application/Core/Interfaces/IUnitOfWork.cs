using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IMemoryRepository MemoryRepository { get; }
        IUserRepository UserRepository { get; }
        IMessagingRepository MessagingRepository { get; }
        IFriendsRepository FriendsRepository { get; }
        INotificationRepository NotificationRepository { get; }
        void SaveChanges();
    }
}
