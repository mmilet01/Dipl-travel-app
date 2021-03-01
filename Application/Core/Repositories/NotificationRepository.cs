using Core.Interfaces;
using Persistance;

namespace Core.Repositories
{
    public class NotificationRepository : GenericRepository<Notifications>, INotificationRepository
    {
        public NotificationRepository(mmiletaContext context) : base(context)
        {
        }
    }
}
