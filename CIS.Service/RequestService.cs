using System;
using System.Collections.Generic;
using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;

namespace CIS.Service
{
    public interface IRequestService
    {
        Request Add(Request request);

        void Update(Request request);

        Request Delete(int id);

        IEnumerable<Request> GetAll();

        Request GetById(int id);

        void SaveChanges();
    }

    public class RequestService : IRequestService
    {
        private IRequestRepository _requestRepository;
        private IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository requestRepository, IUnitOfWork unitOfWork)
        {
            this._requestRepository = requestRepository;
            this._unitOfWork = unitOfWork;
        }

        public Request Add(Request request)
        {
            return _requestRepository.Add(request);
        }

        public Request Delete(int id)
        {
            return _requestRepository.Delete(id);
        }

        public IEnumerable<Request> GetAll()
        {
            return _requestRepository.GetAll();
        }

        public Request GetById(int id)
        {
            return _requestRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Request request)
        {
            _requestRepository.Update(request);
        }
    }
}