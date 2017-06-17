using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Entities;
using System.Collections.Generic;

namespace CatSolution.Application.MainModule.Services.User
{
    public interface IUserManagementService : IManagementService<AspNetUser, AspNetUserDTO>
    {
        IEnumerable<AspNetUserDTO> GetOnlyRoleUser(int workSpaceId);

        AspNetUserDTO ModifyUser(AspNetUser item);

        int AssignCompanies(IEnumerable<SYS_UserCompany> lstUserCompany, string userId, int workSpaceId);

        IEnumerable<SYS_AuthorizationDTO> GetAuthorizationUser(string userId, byte companyId, byte applicationId);

        int SaveAuthorization(List<Dictionary<string, object>> lst, string userId, byte companyId, string userName);
    }
}
