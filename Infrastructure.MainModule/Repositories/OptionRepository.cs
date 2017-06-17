using CatSolution.Domain.MainModule.Contracts.Options;
using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using CatSolution.Infrastructure.MainModule.UnitOfWork;

namespace CatSolution.Infrastructure.MainModule.Repositories
{
    public class OptionRepository : Repository<SYS_Option>, IOptionRepository
    {
        public OptionRepository(IMainUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public void RemoveDetailOption(SYS_DetailOption item)
        {
            var currentUnitOfWork = (IMainUnitOfWork)UnitOfWork;

            currentUnitOfWork.DetailsOption.Attach(item);
            currentUnitOfWork.DetailsOption.Remove(item);
        }
    }
}
