using System;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core.Specification
{
    public sealed class DirectSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class, new()
    {
        Expression<Func<TEntity, bool>> _MatchingCriteria;

        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }
    }
}
