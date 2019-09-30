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
    public interface IJobService
    {
        Job Add(Job job);

        void Update(Job job);

        Job Delete(int id);

        IEnumerable<Job> GetAll();

        IEnumerable<Job> GetAllAvailable();

        Job GetById(int id);
    

        void SaveChanges();
    }

    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private IUnitOfWork _unitOfWork;

        public JobService(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public Job Add(Job job)
        {
            return _jobRepository.Add(job);
        }

        public Job Delete(int id)
        {
            return _jobRepository.Delete(id);
        }

        public IEnumerable<Job> GetAll()
        {
            return _jobRepository.GetAll();
        }

        public IEnumerable<Job> GetAllAvailable()
        {
            return _jobRepository.GetMulti(x => x.Status).OrderBy(x => x.CreatedDate);
        }        

        public Job GetById(int id)
        {
            return _jobRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Job job)
        {
            _jobRepository.Update(job);
        }
    }
}
