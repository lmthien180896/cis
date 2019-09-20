using CIS.Data.Infrastructure;
using CIS.Model.Models;

namespace CIS.Data.Repositories
{
    public interface IRequestReportRepository : IRepository<RequestReport>
    {
    }

    public class RequestReportRepository : RepositoryBase<RequestReport>, IRequestReportRepository
    {
        public RequestReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}