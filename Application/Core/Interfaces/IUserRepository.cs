using Core.Enums;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Users GetByEmail(string email);
    }
}
