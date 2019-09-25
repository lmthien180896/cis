using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System.Linq;

namespace CIS.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public User GetUserByUsername(string username)
        {
            return this.DbContext.Users.Single(x => x.Username == username);
        }
    }
}