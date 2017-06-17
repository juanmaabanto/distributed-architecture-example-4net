using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Entities;
using System.Collections.Generic;

namespace CatSolution.Application.MainModule.Services.Company
{
    public interface ICompanyManagementService : IManagementService<SYS_Company, SYS_CompanyDTO>
    {
        SYS_CompanyDTO AddCompany(SYS_Company item, string source);

        SYS_CompanyDTO ModifyCompany(SYS_Company item);

        int Cancel(byte companyId);

        IEnumerable<SYS_UserCompanyDTO> GetCompanyByUser(string userId);

        IEnumerable<SYS_CompanyDTO> GetCompanyThatNotAssign(string userId, int workSpaceId);
    }
}
