using System;
using System.Collections;
using System.Collections.Generic;
using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System.Linq;

namespace CIS.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {        
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}