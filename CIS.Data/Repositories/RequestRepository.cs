using CIS.Common;
using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CIS.Data.Repositories
{
    public interface IRequestRepository : IRepository<Request>
    {
        IEnumerable<Request> GetAllRequests();

        IEnumerable<Request> GetAllWaitingRequests();

        IEnumerable<Request> GetAllSupportingRequests();

        IEnumerable<Request> GetAllCompletedRequests();
    }

    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return this.DbContext.Requests.ToList().OrderByDescending(x => x.CreatedDate);
        }

        public IEnumerable<Request> GetAllWaitingRequests()
        {
            return this.DbContext.Requests.Where(x => x.Progress == CommonConstant.WaitingProgress).ToList().OrderByDescending(x => x.UpdatedDate);
        }

        public IEnumerable<Request> GetAllSupportingRequests()
        {
            return this.DbContext.Requests.Where(x => x.Progress == CommonConstant.SupportingProgress).ToList().OrderByDescending(x => x.UpdatedDate);
        }

        public IEnumerable<Request> GetAllCompletedRequests()
        {
            return this.DbContext.Requests.Where(x => x.Progress == CommonConstant.CompletedProgress).ToList().OrderByDescending(x => x.UpdatedDate);
        }
    }
}