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
    public interface ISlideService
    {
        Slide Add(Slide slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        Slide GetById(int id);

        void Update(Slide slide);

        void SaveChanges();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public Slide Delete(int id)
        {
            var slide = _slideRepository.GetSingleById(id);
            return _slideRepository.Delete(slide);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
