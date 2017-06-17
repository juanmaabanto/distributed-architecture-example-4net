using System;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core.Specification
{
    public sealed class OrSpecification<TEntity> : CompositeSpecification<TEntity> where TEntity : class, new()
    {
        private ISpecification<TEntity> _RightSideSpecification = null;
        private ISpecification<TEntity> _LeftSideSpecification = null;

        public OrSpecification(ISpecification<TEntity> leftSide, ISpecification<TEntity> rightSide)
        {
            if (leftSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<TEntity>)null)
                throw new ArgumentNullException("rightSide");

            this._LeftSideSpecification = leftSide;
            this._RightSideSpecification = rightSide;
        }

        public override ISpecification<TEntity> LeftSideSpecification
        {
            get { return _LeftSideSpecification; }
        }

        public override ISpecification<TEntity> RightSideSpecification
        {
            get { return _RightSideSpecification; }
        }
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            Expression<Func<TEntity, bool>> left = _LeftSideSpecification.SatisfiedBy();
            Expression<Func<TEntity, bool>> right = _RightSideSpecification.SatisfiedBy();

            return (left.Or(right));
        }
    }
}
