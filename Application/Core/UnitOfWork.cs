using Core.Interfaces;
using Core.Repositories;
using Persistance;

namespace Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly mmiletaContext _context;
        public UnitOfWork(mmiletaContext context)
        {
            _context = context;
        }

        private IMemoryRepository memoryRepository;
        public IMemoryRepository MemoryRepository
        {
            get
            {
                if (memoryRepository == null)
                {
                    memoryRepository = new MemoryRepository(_context);
                }

                return memoryRepository;
            }
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }

        private IMessagingRepository messageRepository;
        public IMessagingRepository MessagingRepository
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new MessagingRepository(_context);
                }
                return messageRepository;
            }
        }

        private IFriendsRepository friendsRepository;
        public IFriendsRepository FriendsRepository
        {
            get
            {
                if (friendsRepository == null)
                {
                    friendsRepository = new FriendsRepository(_context);
                }
                return friendsRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
