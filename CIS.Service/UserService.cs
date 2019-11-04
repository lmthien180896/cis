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

    public interface IUserService
    {
        int CountTotal();

        User Add(User user);

        void Update(User user);

        User Delete(int id);

        IEnumerable<User> GetAll();
      
        User GetById(int id);

        User CheckAuthen(User user);

        User GetUserByUsername(string username);

        List<string> GetCredentials(int groupId);        

        void SaveChanges();
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;     
        private ICredentialRepository _credentialRepository;
        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, ICredentialRepository credentialRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _credentialRepository = credentialRepository;
            _unitOfWork = unitOfWork;
        }

        public int CountTotal()
        {
            return _userRepository.GetAll().Count();
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public User Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public User CheckAuthen(User user)
        {
            var checkUser = _userRepository.GetSingleByCondition(x => x.Username == user.Username && x.Password == user.Password);
            return checkUser;
        }

        public List<string> GetCredentials(int groupId)
        {
            var listRole = _credentialRepository.GetMulti(x => x.UserGroupID == groupId).Select(x => x.RoleID).ToList(); ;
            return listRole;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }       
    }

}
