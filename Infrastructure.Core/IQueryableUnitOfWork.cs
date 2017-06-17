using CatSolution.Domain.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CatSolution.Infrastructure.Core
{
    /// <summary>
    /// El contrato de UnitOfWork para el EF
    /// </summary>
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Devuelve una instancia IDbSet para el acceso a las entidades del tipo dado en el contexto, 
        /// la ObjectStateManager, y el almacén subyacente.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// Adjunta el item dentro del "ObjectStateManager".
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad.</typeparam>
        /// <param name="item">El item.</param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Setea el item como modificado.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad.</typeparam>
        /// <param name="item">El item.</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Aplica valores actuales en <paramref name="original"/>
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad.</typeparam>
        /// <param name="original">La entidad original.</param>
        /// <param name="current">La entidad actual.</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;

        /// <summary>
        /// Ejecuta consulta Sql.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad.</typeparam>
        /// <param name="sqlQuery">Consulta que se realizara.</param>
        /// <param name="parameters">Parametros de la consulta.</param>
        /// <returns>Enumerable de la entidad.</returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Ejecuta comando sql.
        /// </summary>
        /// <param name="sqlCommand">Comando que se ejecutara.</param>
        /// <param name="parameters">Parametros para el comando sql.</param>
        /// <returns>Resultado de la ejecución.</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        /// <summary>
        /// Asíncronamente ejecuta comando sql.
        /// </summary>
        /// <param name="sqlCommand">Comando que se ejecutara.</param>
        /// <param name="parameters">Parametros para el comando sql.</param>
        /// <returns>Resultado de la ejecución.</returns>
        Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);

    }
}
