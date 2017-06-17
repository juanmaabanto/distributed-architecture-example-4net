using System;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core.Specification
{
    public sealed class TrueSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class, new()
    {
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }
    }
}
