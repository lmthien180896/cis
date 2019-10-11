using System;
using System.Collections.Generic;
using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;

namespace CIS.Service
{
    public interface IRequestCategoryService
    {        
        RequestCategory Add(RequestCategory requestCategory);

        void Update(RequestCategory requestCategory);

        RequestCategory Delete(int id);

        IEnumerable<RequestCategory> GetAll();

        RequestCategory GetById(int id);         

        void SaveChanges();
    }

    public class RequestCategoryService : IRequestCategoryService
    {
        private IRequestCategoryRepository _requestCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public RequestCategoryService(IRequestCategoryRepository requestCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._requestCategoryRepository = requestCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public RequestCategory Add(RequestCategory requestCategory)
        {
            if (_requestCategoryRepository.Count(x => x.Name == requestCategory.Name) == 0)
            {
                return _requestCategoryRepository.Add(requestCategory);             
            }
            else
                return null;
        }

        public RequestCategory Delete(int id)
        {
            return _requestCategoryRepository.Delete(id);
        }

        public IEnumerable<RequestCategory> GetAll()
        {
            return _requestCategoryRepository.GetAll();
        }
       

        public RequestCategory GetById(int id)
        {
            return _requestCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(RequestCategory requestCategory)
        {
            _requestCategoryRepository.Update(requestCategory);
        }
    }
}