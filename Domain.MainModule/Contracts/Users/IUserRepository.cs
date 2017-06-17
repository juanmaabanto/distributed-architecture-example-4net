using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Entities;
using System.Collections.Generic;

namespace CatSolution.Domain.MainModule.Contracts.Users
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        AspNetRole GetRole(string name);

        bool IsValidAdd(int workSpaceId);

        void RemoveUserCompany(SYS_UserCompany entity);

        IEnumerable<SYS_Authorization> GetAuthorizationUser(string userId, byte companyId, byte applicationId);

        SYS_Authorization FindAuthorization(string userId, short optionId, byte companyId);

        SYS_DetailAuthorization FindDetailAuthorization(string userId, short optionId, byte companyId, short detailOptionId);

        SYS_Authorization AddAuthorization(SYS_Authorization item);

        SYS_DetailAuthorization AddDetailAuthorization(SYS_DetailAuthorization item);

        void ModifyAuthorization(SYS_Authorization item);

        void ModifyDetailAuthorization(SYS_DetailAuthorization item);

        void RemoveAuthorization(SYS_Authorization item);

        void RemoveDetailAuthorization(SYS_DetailAuthorization item);
    }
}
