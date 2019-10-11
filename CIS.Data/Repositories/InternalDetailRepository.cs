using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface IInternalDetailRepository : IRepository<InternalDetail>
    {
    }

    public class InternalDetailRepository : RepositoryBase<InternalDetail>, IInternalDetailRepository
    {
        public InternalDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}