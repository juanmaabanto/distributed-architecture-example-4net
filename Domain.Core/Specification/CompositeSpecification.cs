namespace CatSolution.Domain.Core.Specification
{
    public abstract class CompositeSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class, new()
    {
        public abstract ISpecification<TEntity> LeftSideSpecification { get; }
        public abstract ISpecification<TEntity> RightSideSpecification { get; }
    }
}
