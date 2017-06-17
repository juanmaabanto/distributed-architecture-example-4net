using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.MainModule.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace CatSolution.Infrastructure.MainModule.UnitOfWork
{
    public class MainUnitOfWork : MainContext, IMainUnitOfWork
    {

        #region Miembros IDbSet

        IDbSet<AspNetRole> _roles;
        public IDbSet<AspNetRole> Roles
        {
            get
            {
                if (_roles == null)
                    _roles = base.Set<AspNetRole>();
                return _roles;
            }
        }

        IDbSet<AspNetUser> _users;
        public IDbSet<AspNetUser> Users
        {
            get
            {
                if (_users == null)
                    _users = base.Set<AspNetUser>();
                return _users;
            }
        }

        IDbSet<SYS_Application> _applications;
        public IDbSet<SYS_Application> Applications
        {
            get
            {
                if (_applications == null)
                    _applications = base.Set<SYS_Application>();
                return _applications;
            }
           
        }

        IDbSet<SYS_Authorization> _authorizations;
        public IDbSet<SYS_Authorization> Authorizations
        {
            get
            {
                if (_authorizations == null)
                    _authorizations = base.Set<SYS_Authorization>();
                return _authorizations;
            }
        }

        IDbSet<SYS_Company> _companies;
        public IDbSet<SYS_Company> Companies
        {
            get
            {
                if (_companies == null)
                    _companies = base.Set<SYS_Company>();
                return _companies;
            }
        }

        IDbSet<SYS_DetailAuthorization> _detailsAuthorization;
        public IDbSet<SYS_DetailAuthorization> DetailsAuthorization
        {
            get
            {
                if (_detailsAuthorization == null)
                    _detailsAuthorization = base.Set<SYS_DetailAuthorization>();
                return _detailsAuthorization;
            }
        }

        IDbSet<SYS_DetailOption> _detailsOption;
        public IDbSet<SYS_DetailOption> DetailsOption
        {
            get
            {
                if (_detailsOption == null)
                    _detailsOption = base.Set<SYS_DetailOption>();
                return _detailsOption;
            }
        }

        IDbSet<SYS_DetailWorkSpace> _detailsWorkSpace;
        public IDbSet<SYS_DetailWorkSpace> DetailsWorkSpace
        {
            get
            {
                if (_detailsWorkSpace == null)
                    _detailsWorkSpace = base.Set<SYS_DetailWorkSpace>();
                return _detailsWorkSpace;
            }
        }

        IDbSet<SYS_Module> _modules;
        public IDbSet<SYS_Module> Modules
        {
            get
            {
                if (_modules == null)
                    _modules = base.Set<SYS_Module>();
                return _modules;
            }
        }

        IDbSet<SYS_Option> _options;
        public IDbSet<SYS_Option> Options
        {
            get
            {
                if (_options == null)
                    _options = base.Set<SYS_Option>();
                return _options;
            }
        }

        IDbSet<SYS_UserCompany> _usersCompany;
        public IDbSet<SYS_UserCompany> UsersCompany
        {
            get
            {
                if (_usersCompany == null)
                    _usersCompany = base.Set<SYS_UserCompany>();
                return _usersCompany;
            }
        }

        IDbSet<SYS_WorkSpace> _workSpaces;
        public IDbSet<SYS_WorkSpace> WorkSpaces
        {
            get
            {
                if (_workSpaces == null)
                    _workSpaces = base.Set<SYS_WorkSpace>();
                return _workSpaces;
            }
        }

        IDbSet<SYS_WorkSpaceType> _workSpaceTypes;
        public IDbSet<SYS_WorkSpaceType> WorkSpaceTypes
        {
            get
            {
                if (_workSpaceTypes == null)
                    _workSpaceTypes = base.Set<SYS_WorkSpaceType>();
                return _workSpaceTypes;
            }
        }

        #endregion

        #region Miembros IQueryableUnitOfWork

        public IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            if (Entry(item).State == EntityState.Detached)
            {
                base.Set<TEntity>().Attach(item);
            }
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            Entry(original).CurrentValues.SetValues(current);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommandAsync(sqlCommand, parameters);
        }

        #endregion

        #region Miembros de Unit Of Work

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            DbEntityValidationResult validationResult = base.Entry(item).GetValidationResult();

            Entry(item).State = EntityState.Modified;
        }

        public int Commit()
        {
            
            return base.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }

        public void RollbackChanges()
        {
            base.ChangeTracker.Entries()
                                  .ToList()
                                  .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        #region DbContext Overrides


        public new void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
