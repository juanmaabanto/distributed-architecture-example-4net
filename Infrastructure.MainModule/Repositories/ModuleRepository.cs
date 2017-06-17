using CatSolution.Domain.MainModule.Contracts.modules;
using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using CatSolution.Infrastructure.MainModule.UnitOfWork;

namespace CatSolution.Infrastructure.MainModule.Repositories
{
    public class ModuleRepository : Repository<SYS_Module>, IModuleRepository
    {
        public ModuleRepository(IMainUnitOfWork unitofwork) : base(unitofwork)
        {

        }
    }
}
