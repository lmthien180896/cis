using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Service
{
    public interface IApplicantService
    {
        Applicant Add(Applicant applicant);

        void Update(Applicant applicant);

        Applicant Delete(int id);

        IEnumerable<Applicant> GetAll();

        Applicant GetById(int id);

        void SaveChanges();
    }

    public class ApplicantService : IApplicantService
    {
        private IApplicantRepository _applicantRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicantService(IApplicantRepository applicantRepository, IUnitOfWork unitOfWork)
        {
            _applicantRepository = applicantRepository;
            _unitOfWork = unitOfWork;
        }

        public Applicant Add(Applicant applicant)
        {
            return _applicantRepository.Add(applicant);
        }

        public Applicant Delete(int id)
        {
            return _applicantRepository.Delete(id);
        }

        public IEnumerable<Applicant> GetAll()
        {
            return _applicantRepository.GetAll();
        }
      
        public Applicant GetById(int id)
        {
            return _applicantRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Applicant applicant)
        {
            _applicantRepository.Update(applicant);
        }
    }
}
