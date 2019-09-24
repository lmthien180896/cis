using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface ICredentialRepository : IRepository<Credential>
    {
    }

    public class CredentialRepository : RepositoryBase<Credential>, ICredentialRepository
    {
        public CredentialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}