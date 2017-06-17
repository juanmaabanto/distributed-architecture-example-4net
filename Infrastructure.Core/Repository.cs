using CatSolution.Domain.Core;
using CatSolution.Domain.Core.Specification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace CatSolution.Infrastructure.Core
{
    /// <summary>
    /// Clase base de repositorios.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad del repositorio.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        #region Miembros

        IQueryableUnitOfWork _UnitOfWork = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Crea una nueva instancia del repositorio.
        /// </summary>
        /// <param name="unitOfWork">Asociado con el "Unit Of Work".</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region Miembros de IRepository

        public IUnitOfWork UnitOfWork
        {
            get { return _UnitOfWork; }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository"/>
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TEntity item)
        {
            if (item != null)
            {
                GetSet().Add(item);
            }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository"/>
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                _UnitOfWork.Attach(item);
                GetSet().Remove(item);
            }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository"/>
        /// </summary>
        /// <param name="item"></param>
        public virtual void Modify(TEntity item)
        {
            if (item != null)
            {
                _UnitOfWork.SetModified(item);
            }
        }

        public virtual void Modify(ICollection<TEntity> items)
        {
            //for each element in collection apply changes
            foreach (TEntity item in items)
            {
                if (item != null)
                {
                    _UnitOfWork.SetModified(item);
                }
            }
        }

        public virtual void Attach(TEntity item)
        {
            _UnitOfWork.Attach(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        public TEntity GetById(params object[] keys)
        {
            return GetSet().Find(keys);
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return GetSet().Where(predicate);
        }

        public IEnumerable<TEntity> GetBySpecification(ISpecification<TEntity> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException("specification");
            }

            return GetSet().Where(specification.SatisfiedBy()).AsEnumerable();
        }

        public IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, bool ascending)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException("Invalido indice de página.", "pageIndex");
            }

            if (pageCount <= 0)
            {
                throw new ArgumentException("Cantidad de páginas inválidas.", "pageCount");
            }

            if (orderByExpression == null)
            {
                throw new ArgumentNullException("orderByExpression", "La expresión no puede ser null.");
            }

            return ( ascending ? GetSet().OrderBy(orderByExpression).Skip(pageIndex * pageCount).Take(pageCount).ToList() 
                : GetSet().OrderByDescending(orderByExpression).Skip(pageIndex * pageCount).Take(pageCount).ToList() );
        }

        #endregion

        #region Miembros IDisposable

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_UnitOfWork != null)
                {
                    _UnitOfWork.Dispose();
                    _UnitOfWork = null;
                }
            }
        }

        #endregion

        #region Metodos Privados

        IDbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>();
        }

        #endregion

    }
}
