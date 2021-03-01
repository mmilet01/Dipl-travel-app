using Core.Interfaces;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
   public class MessagingRepository : GenericRepository<Messages>, IMessagingRepository
   {
        public MessagingRepository(mmiletaContext _context) : base(_context)
        {

        }
    }
}
