using System;
using System.Linq;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core.Specification
{
    public sealed class NotSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class, new()
    {
        Expression<Func<TEntity, bool>> _OriginalCriteria;
        public NotSpecification(ISpecification<TEntity> originalSpecification)
        {

            if (originalSpecification == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("originalSpecification");

            _OriginalCriteria = originalSpecification.SatisfiedBy();
        }
        public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
        {
            if (originalSpecification == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("originalSpecification");

            _OriginalCriteria = originalSpecification;
        }
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {

            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_OriginalCriteria.Body), _OriginalCriteria.Parameters.Single());
        }
    }
}
