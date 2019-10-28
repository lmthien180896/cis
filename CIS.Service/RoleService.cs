using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System.Collections.Generic;

namespace CIS.Service
{
    public interface IRoleService
    {
        Role Add(Role role);

        void Update(Role role);

        Role Delete(int id);

        IEnumerable<Role> GetAll();

        Role GetById(string id);        

        void SaveChanges();
    }

    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;
        private IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public Role Add(Role role)
        {
            return _roleRepository.Add(role);
        }

        public Role Delete(int id)
        {
            return _roleRepository.Delete(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetById(string id)
        {
            return _roleRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }       
    }
}