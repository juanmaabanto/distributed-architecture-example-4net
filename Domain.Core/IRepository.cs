using CatSolution.Domain.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CatSolution.Domain.Core
{
    /// <summary>
    /// Interfaz base para implementar un "Repository Pattern".
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad para este repositorio.</typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, new()
    {
        /// <summary>
        /// Obtiene el 'unit of work' para este repositorio.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Agrega un item al repositorio.
        /// </summary>
        /// <param name="item">Item que se va agregar al repositorio.</param>
        void Add(TEntity item);

        /// <summary>
        /// Elimina item.
        /// </summary>
        /// <param name="item">Item que se va eliminar del repositorio.</param>
        void Remove(TEntity item);

        /// <summary>
        /// Modifica item del repositorio.
        /// </summary>
        /// <param name="item">Item que se va modificar.</param>
        void Modify(TEntity item);

        /// <summary>
        /// Modifica una colección de items del repositorio.
        /// </summary>
        /// <param name="items">Colección de items que se modificaran.</param>
        void Modify(ICollection<TEntity> items);

        /// <summary>
        /// Adjunta el item dentro del "ObjectStateManager".
        /// </summary>
        /// <param name="item">El item.</param>
        void Attach(TEntity item);

        /// <summary>
        /// Obtiene elementos del tipo TEntity del repositorio.
        /// </summary>
        /// <returns>Lista de elementos.</returns>
        IEnumerable<TEntity> GetAll();

        TEntity GetById(params object[] keys);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity,bool>> predicate);

        /// <summary>
        /// Obtiene elementos del tipo TEntity del repositorio que cumplan una especificación.
        /// </summary>
        /// <param name="specification">Especificación que se debe cumplir.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetBySpecification(ISpecification<TEntity> specification);

        /// <summary>
        /// Obtiene elementos del tipo TEntity del repositorio páginados.
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">Número de página.</param>
        /// <param name="pageCount">Cantidad de elementos por página.</param>
        /// <param name="orderByExpression">Especificación de orden.</param>
        /// <param name="ascending">Es true si se devuelven elementos ordenados ascendentemente.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, bool ascending);

    }
}
