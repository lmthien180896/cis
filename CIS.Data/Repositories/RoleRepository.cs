using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
    }

    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}