using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Entities;
using System.Collections.Generic;

namespace CatSolution.Domain.MainModule.Contracts.Companies
{
    public interface ICompanyRepository : IRepository<SYS_Company>
    {
        bool IsValidAdd(int workSpaceId);

        IEnumerable<SYS_UserCompany> GetCompanyByUser(string userId);

        IEnumerable<SYS_Company> GetCompanyThatNotAssign(string userId, int workSpaceId);
    }
}
