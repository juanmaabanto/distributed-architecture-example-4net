using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Application.MainModule.Services.Module
{
    public interface IModuleManagementService : IManagementService<SYS_Module, SYS_ModuleDTO>
    {
    }
}
