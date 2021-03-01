using Core.Interfaces;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Repositories
{
    public class MemoryRepository : GenericRepository<Memories>, IMemoryRepository
    {
        public MemoryRepository(mmiletaContext _context) : base(_context)
        {

        }
        
    }
}
