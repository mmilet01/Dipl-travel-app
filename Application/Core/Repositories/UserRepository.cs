using Core.Enums;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(mmiletaContext _context) : base(_context)
        {
        }

        public Users GetByEmail(string email)
        {   
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
