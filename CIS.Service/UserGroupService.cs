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
    public interface IUserGroupService
    {
        IEnumerable<UserGroup> GetAll();
        
        UserGroup GetById(int id);

        void SaveChanges();
    }

    public class UserGroupService : IUserGroupService
    {
        private IUserGroupRepository _userGroupRepository;
        private IUnitOfWork _unitOfWork;

        public UserGroupService(IUserGroupRepository userGroupRepository, IUnitOfWork unitOfWork)
        {
            _userGroupRepository = userGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserGroup> GetAll()
        {
            return _userGroupRepository.GetAll();
        }

        public UserGroup GetById(int id)
        {
            return _userGroupRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
        
    }
}
