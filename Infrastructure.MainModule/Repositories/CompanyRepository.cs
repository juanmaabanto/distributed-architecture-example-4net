using CatSolution.Domain.MainModule.Contracts.Companies;
using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using CatSolution.Infrastructure.MainModule.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatSolution.Infrastructure.MainModule.Repositories
{
    public class CompanyRepository : Repository<SYS_Company>, ICompanyRepository
    {
        public CompanyRepository(IMainUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public bool IsValidAdd(int workSpaceId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            var workspace = currentUnitOfWork.WorkSpaces.Find(workSpaceId);
            int countCompanies = currentUnitOfWork.Companies.Count(c => c.WorkSpaceId == workSpaceId && !c.Canceled);

            if (workspace == null)
            {
                throw new ArgumentNullException("workspace");
            }

            return workspace.MaxCompanies > countCompanies;
        }

        public IEnumerable<SYS_UserCompany> GetCompanyByUser(string userId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            var list = currentUnitOfWork.UsersCompany.Where(uc => uc.UserId == userId && !uc.SYS_Company.Canceled);

            return list;
        }

        public IEnumerable<SYS_Company> GetCompanyThatNotAssign(string userId, int workSpaceId)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            var companies = currentUnitOfWork.Companies.Where(c => c.WorkSpaceId == workSpaceId && !c.Canceled);
            var assign = currentUnitOfWork.UsersCompany.Where(ac => ac.UserId == userId);

            var list = (from c in companies
                        join uc in assign on c.CompanyId equals uc.CompanyId into records
                        from r in records.DefaultIfEmpty()
                        where r == null
                        select c);

            return list;
        }
    }
}
