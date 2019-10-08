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
    public interface IFeedbackService
    {
        Feedback Add(Feedback feedback);

        void Update(Feedback feedback);

        Feedback Delete(int id);

        IEnumerable<Feedback> GetAll();

        Feedback GetById(int id);

        int CountTotal();

        void SaveChanges();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public int CountTotal()
        {
            return _feedbackRepository.GetAll().Count();
        }

        public Feedback Add(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }
        
        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}
