using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Contracts.modules;
using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Application.MainModule.Services.Module
{
    public class ModuleManagementService : ManagementService<SYS_Module, SYS_ModuleDTO>, IModuleManagementService
    {
        IModuleRepository _ModuleRepository;

        public ModuleManagementService(IModuleRepository moduleRepository) :base(moduleRepository)
        {
            _ModuleRepository = moduleRepository;
        }
    }
}
