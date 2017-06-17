using CatSolution.Domain.MainModule.Contracts.Applications;
using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using CatSolution.Infrastructure.MainModule.UnitOfWork;

namespace CatSolution.Infrastructure.MainModule.Repositories
{
    public class ApplicationRepository : Repository<SYS_Application>, IApplicationRepository
    {
        public ApplicationRepository(IMainUnitOfWork unitofwork) : base(unitofwork)
        {

        }
    }
}
