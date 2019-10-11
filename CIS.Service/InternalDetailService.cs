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
    public interface IInternalDetailService
    {
        IEnumerable<InternalDetail> GetAll();

        InternalDetail GetById(int id);

        InternalDetail Add(InternalDetail internalDetail);

        InternalDetail Delete(int id);

        void Update(InternalDetail internalDetail);

        void SaveChanges();
    }

    public class InternalDetailService : IInternalDetailService
    {
        private IInternalDetailRepository _internalDetailRepository;
        private IUnitOfWork _unitOfWork;

        public InternalDetailService(IInternalDetailRepository internalDetailRepository, IUnitOfWork unitOfWork)
        {
            _internalDetailRepository = internalDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<InternalDetail> GetAll()
        {
            return _internalDetailRepository.GetAll();
        }

        public InternalDetail GetById(int id)
        {
            return _internalDetailRepository.GetSingleById(id);
        }

        public InternalDetail Delete(int id)
        {
            return _internalDetailRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public InternalDetail Add(InternalDetail internalDetail)
        {
            return _internalDetailRepository.Add(internalDetail);
        }

        public void Update(InternalDetail internalDetail)
        {
            _internalDetailRepository.Update(internalDetail);
        }
    }
}
