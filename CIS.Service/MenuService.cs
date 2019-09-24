using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System.Collections.Generic;

namespace CIS.Service
{
    public interface IMenuService
    {
        Menu Add(Menu menu);

        void Update(Menu menu);

        Menu Delete(int id);

        IEnumerable<Menu> GetAll();

        IEnumerable<Menu> GetAll(int groupId);

        Menu GetById(int id);

        void SaveChanges();
    }

    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;
        private IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public Menu Add(Menu menu)
        {
            return _menuRepository.Add(menu);
        }

        public Menu Delete(int id)
        {
            return _menuRepository.Delete(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public IEnumerable<Menu> GetAll(int groupId)
        {
            return _menuRepository.GetMulti(x => x.GroupID == groupId);
        }

        public Menu GetById(int id)
        {
            return _menuRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Menu menu)
        {
            _menuRepository.Update(menu);
        }
    }
}