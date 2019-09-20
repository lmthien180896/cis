using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface IRequestCategoryRepository : IRepository<RequestCategory>
    {
    }

    public class RequestCategoryRepository : RepositoryBase<RequestCategory>, IRequestCategoryRepository
    {
        public RequestCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}