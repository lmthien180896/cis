using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System.Collections.Generic;

namespace CIS.Service
{
    public interface ICredentialService
    {
        Credential Add(Credential credential);

        void Update(Credential credential);

        Credential Delete(int id);

        IEnumerable<Credential> GetAll();

        Credential GetById(int id);

        bool Check(int userGroupID, string roleID);

        Credential GetCredential(int userGroupID, string roleID);

        void SaveChanges();
    }

    public class CredentialService : ICredentialService
    {
        private ICredentialRepository _credentialRepository;
        private IUnitOfWork _unitOfWork;

        public CredentialService(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork)
        {
            _credentialRepository = credentialRepository;
            _unitOfWork = unitOfWork;
        }

        public Credential Add(Credential credential)
        {
            return _credentialRepository.Add(credential);
        }

        public bool Check(int userGroupID, string roleID)
        {
            var result = _credentialRepository.GetSingleByCondition(x => x.RoleID == roleID && x.UserGroupID == userGroupID);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Credential Delete(int id)
        {
            return _credentialRepository.Delete(id);
        }

        public IEnumerable<Credential> GetAll()
        {
            return _credentialRepository.GetAll();
        }

        public Credential GetById(int id)
        {
            return _credentialRepository.GetSingleById(id);
        }

        public Credential GetCredential(int userGroupID, string roleID)
        {
            return _credentialRepository.GetSingleByCondition(x => x.UserGroupID == userGroupID && x.RoleID == roleID);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Credential credential)
        {
            _credentialRepository.Update(credential);
        }
    }
}