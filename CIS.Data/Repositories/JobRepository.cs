using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Data.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
    }

    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
