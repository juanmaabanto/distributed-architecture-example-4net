using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatSolution.Application.Core
{
    public interface IManagementService<TEntity, TEntityDTO>
        where TEntity : class, new()
        where TEntityDTO : class, new()
    {
        TEntityDTO Add(TEntity entity);

        Task<TEntityDTO> AddAsync(TEntity entity);

        TEntityDTO Modify(TEntity entity);

        Task<TEntityDTO> ModifyAsync(TEntity entity);

        int Modify(ICollection<TEntity> items);

        Task<int> ModifyAsync(ICollection<TEntity> items);

        int Remove(params object[] keys);

        Task<int> RemoveAsync(params object[] keys);

        TEntityDTO GetById(params object[] keys);

        IEnumerable<TEntityDTO> GetAll();

        IEnumerable<TEntityDTO> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
