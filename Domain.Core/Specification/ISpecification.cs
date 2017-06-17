using System;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core.Specification
{
    
    public interface ISpecification<TEntity>
        where TEntity: class, new()
    {
        Expression<Func<TEntity,bool>> SatisfiedBy();
    }
}
