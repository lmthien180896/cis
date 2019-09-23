using System;
using System.Collections.Generic;
using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;

namespace CIS.Service
{
    public interface IRequestReportService
    {
        RequestReport Add(RequestReport requestReport);

        void Update(RequestReport requestReport);

        RequestReport Delete(int id);

        IEnumerable<RequestReport> GetAll();

        IEnumerable<RequestReport> GetAll(int id);

        RequestReport GetById(int id);

        void SaveChanges();
    }

    public class RequestReportService : IRequestReportService
    {
        private IRequestReportRepository _requestReportRepository;
        private IUnitOfWork _unitOfWork;

        public RequestReportService(IRequestReportRepository requestReportRepository, IUnitOfWork unitOfWork)
        {
            this._requestReportRepository = requestReportRepository;
            this._unitOfWork = unitOfWork;
        }

        public RequestReport Add(RequestReport requestReport)
        {
            return _requestReportRepository.Add(requestReport);
        }

        public RequestReport Delete(int id)
        {
            return _requestReportRepository.Delete(id);
        }

        public IEnumerable<RequestReport> GetAll()
        {
            return _requestReportRepository.GetAll();
        }

        public IEnumerable<RequestReport> GetAll(int id)
        {
            return _requestReportRepository.GetMulti(x => x.RequestID == id);
        }

        public RequestReport GetById(int id)
        {
            return _requestReportRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(RequestReport requestReport)
        {
            _requestReportRepository.Update(requestReport);
        }
    }
}