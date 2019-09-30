using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Data.Repositories
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
    }

    public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
