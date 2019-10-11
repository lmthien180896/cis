using System;
using System.Web;
using System.Collections.Generic;
using System.Linq.Expressions;
using CIS.Common;
using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System.Linq;

namespace CIS.Service
{
    public interface IRequestService
    {
        int CountTotal();

        int CountRequestPerMonth(int month);

        int CountByCategory(int categoryId);

        Request Add(Request request);

        void Update(Request request);

        Request Delete(int id);

        int CountByProgress(string progress);

        IEnumerable<Request> GetAll();

        IEnumerable<Request> GetAllRequests();

        IEnumerable<Request> GetAllWaitingRequests();

        IEnumerable<Request> GetAllSupportingRequests();

        IEnumerable<Request> GetAllCompletedRequests();

        IEnumerable<Request> GetMany(Expression<Func<Request, bool>> where, string includes);

        Request GetById(int id);

        void Support(int id, string currentUserName);

        void Close(int id);

        IEnumerable<Request> GetAllFromDate(DateTime? fromDate);

        IEnumerable<Request> GetAllToDate(DateTime? toDate);

        IEnumerable<Request> GetAllRequests(DateTime? fromDate, DateTime? toDate);

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

        public int CountTotal()
        {
            return _requestRepository.GetAll().Count();
        }

        public int CountRequestPerMonth(int month)
        {
            return _requestRepository.GetMulti(x => x.CreatedDate.Value.Month == month).Count();
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

        public int CountByProgress(string progress)
        {
            return _requestRepository.Count(x => x.Progress == progress);
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
            request.ClosedDate = DateTime.Now;
            _requestRepository.Update(request);

            RequestReport closedReport = new RequestReport();
            closedReport.RequestID = id;
            closedReport.SupporterID = _requestReportRepository.GetSingleByCondition(x => x.RequestID == id && x.SupporterID != 0).SupporterID;
            closedReport.Note = "Xác nhận đóng yêu cầu";
            closedReport.CreatedDate = DateTime.Now;
            _requestReportRepository.Add(closedReport);

            _unitOfWork.Commit();
        }

        public int CountByCategory(int categoryId)
        {
            return _requestRepository.GetMulti(x => x.CategoryID == categoryId).Count();
        }

        public IEnumerable<Request> GetAllFromDate(DateTime? fromDate)
        {
            return _requestRepository.GetAllFromDate(fromDate) ;
        }

        public IEnumerable<Request> GetAllToDate(DateTime? toDate)
        {
            return _requestRepository.GetAllToDate(toDate);
        }

        public IEnumerable<Request> GetAllRequests(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null && toDate == null)
                return _requestRepository.GetAllRequests();
            if (fromDate != null && toDate == null)
                return _requestRepository.GetAllFromDate(fromDate);
            if (fromDate == null && toDate != null)
                return _requestRepository.GetAllToDate(toDate);
            else
                return _requestRepository.GetMulti(x => x.CreatedDate <= toDate && x.CreatedDate >= fromDate);
        }
    }
}