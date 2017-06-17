using CatSolution.Domain.MainModule.Contracts.Users;
using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using CatSolution.Infrastructure.MainModule.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CatSolution.Infrastructure.MainModule.Repositories
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        public UserRepository(IMainUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public AspNetRole GetRole(string name)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            var userRole = currentUnitOfWork.Roles.Where(r => r.Name == name).FirstOrDefault();

            return userRole;
        }

        public bool IsValidAdd(int workSpaceId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            var workspace = currentUnitOfWork.WorkSpaces.Find(workSpaceId);
            int countUsers = currentUnitOfWork.Users.Where(u => u.WorkSpaceId == workSpaceId).Count();

            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }

            return workspace.MaxUsers > countUsers;
        }

        public void RemoveUserCompany(SYS_UserCompany entity)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            currentUnitOfWork.UsersCompany.Remove(entity);
        }

        public IEnumerable<SYS_Authorization> GetAuthorizationUser(string userId, byte companyId, byte applicationId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            var lst = currentUnitOfWork.Authorizations.Where(auth => auth.UserId == userId && auth.CompanyId == companyId && auth.SYS_Option.ApplicationId == applicationId);

            return lst;
        }

        public SYS_Authorization FindAuthorization(string userId, short optionId, byte companyId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            var entity = currentUnitOfWork.Authorizations.Find(userId, optionId, companyId);

            return entity;
        }

        public SYS_DetailAuthorization FindDetailAuthorization(string userId, short optionId, byte companyId, short detailOptionId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            var entity = currentUnitOfWork.DetailsAuthorization.Find(userId, optionId, companyId, detailOptionId);

            return entity;
        }

        public SYS_Authorization AddAuthorization(SYS_Authorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            return currentUnitOfWork.Authorizations.Add(item);
        }

        public SYS_DetailAuthorization AddDetailAuthorization(SYS_DetailAuthorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            return currentUnitOfWork.DetailsAuthorization.Add(item);
        }

        public void ModifyAuthorization(SYS_Authorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            currentUnitOfWork.SetModified(item);
        }

        public void ModifyDetailAuthorization(SYS_DetailAuthorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            currentUnitOfWork.SetModified(item);
        }

        public void RemoveAuthorization(SYS_Authorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            currentUnitOfWork.Authorizations.Remove(item);
        }

        public void RemoveDetailAuthorization(SYS_DetailAuthorization item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;
            currentUnitOfWork.DetailsAuthorization.Remove(item);
        }
    }
}
