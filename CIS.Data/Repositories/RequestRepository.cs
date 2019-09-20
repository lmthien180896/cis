using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface IRequestRepository : IRepository<Request>
    {
    }

    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}