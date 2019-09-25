using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CIS.Common;
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

        IEnumerable<Request> GetAllRequests();

        IEnumerable<Request> GetAllWaitingRequests();

        IEnumerable<Request> GetAllSupportingRequests();

        IEnumerable<Request> GetAllCompletedRequests();

        IEnumerable<Request> GetMany(Expression<Func<Request, bool>> where, string includes);

        Request GetById(int id);

        void Support(int id, string currentUserName);

        void Close(int id);

        void SaveChanges();
    }

    public class RequestService : IRequestService
    {
        private IRequestRepository _requestRepository;
        private IRequestReportRepository _requestReportRepository;
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository requestRepository, IRequestReportRepository requestReportRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this._requestRepository = requestRepository;
            this._unitOfWork = unitOfWork;
            this._requestReportRepository = requestReportRepository;
            this._userRepository = userRepository;
        }

        public Request Add(Request request)
        {

            return _requestRepository.Add(request);
        }

        public Request Delete(int id)
        {
            _requestReportRepository.DeleteMulti(x => x.RequestID == id);
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

        public IEnumerable<Request> GetMany(Expression<Func<Request, bool>> where, string includes)
        {
            return _requestRepository.GetMulti(where, null);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Request request)
        {
            _requestRepository.Update(request);
        }

        public void Support(int id, string currentUserName)
        {
            var request = _requestRepository.GetSingleById(id);            
            request.Progress = CommonConstant.SupportingProgress;
            request.UpdatedDate = DateTime.Now;
            request.UpdatedBy = currentUserName;           
            _requestRepository.Update(request);

            RequestReport report = new RequestReport();
            report.CreatedDate = DateTime.Now;
            report.Note = "Bắt đầu xử lí yêu cầu";
            report.RequestID = request.ID;
            report.SupporterID = _userRepository.GetUserByUsername(currentUserName).ID;
            _requestReportRepository.Add(report);

            _unitOfWork.Commit();
        }      

        public IEnumerable<Request> GetAllRequests()
        {
            return _requestRepository.GetAllRequests();
        }

        public IEnumerable<Request> GetAllWaitingRequests()
        {
            return _requestRepository.GetAllWaitingRequests();
        }
        public IEnumerable<Request> GetAllSupportingRequests()
        {
            return _requestRepository.GetAllSupportingRequests();
        }
        public IEnumerable<Request> GetAllCompletedRequests()
        {
            return _requestRepository.GetAllCompletedRequests();
        }

        public void Close(int id)
        {
            var request = _requestRepository.GetSingleById(id);
            request.Progress = CommonConstant.CompletedProgress;
            _requestRepository.Update(request);

            RequestReport closedReport = new RequestReport();
            closedReport.RequestID = id;
            closedReport.Note = "Xác nhận đóng yêu cầu";
            closedReport.CreatedDate = DateTime.Now;
            _requestReportRepository.Add(closedReport);

            _unitOfWork.Commit();
        }
    }
}