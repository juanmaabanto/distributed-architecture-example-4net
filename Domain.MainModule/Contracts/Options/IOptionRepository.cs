using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Domain.MainModule.Contracts.Options
{
    public interface IOptionRepository : IRepository<SYS_Option>
    {
        void RemoveDetailOption(SYS_DetailOption item);
    }
}
