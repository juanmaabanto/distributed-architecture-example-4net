using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Application.MainModule.Services.Option
{
    public interface IOptionManagementService : IManagementService<SYS_Option, SYS_OptionDTO>
    {
        SYS_OptionDTO ModifyOption(SYS_Option item);
    }
}
