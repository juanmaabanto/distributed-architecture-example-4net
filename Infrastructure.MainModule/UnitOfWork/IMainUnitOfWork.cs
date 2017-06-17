using CatSolution.Domain.MainModule.Entities;
using CatSolution.Infrastructure.Core;
using System.Data.Entity;

namespace CatSolution.Infrastructure.MainModule.UnitOfWork
{
    public interface IMainUnitOfWork : IQueryableUnitOfWork
    {
        IDbSet<AspNetRole> Roles { get; }
        IDbSet<AspNetUser> Users { get; }
        IDbSet<SYS_Application> Applications { get; }
        IDbSet<SYS_Authorization> Authorizations { get; }
        IDbSet<SYS_Company> Companies { get; }
        IDbSet<SYS_DetailAuthorization> DetailsAuthorization { get; }
        IDbSet<SYS_DetailOption> DetailsOption { get; }
        IDbSet<SYS_DetailWorkSpace> DetailsWorkSpace { get; }
        IDbSet<SYS_Module> Modules { get; }
        IDbSet<SYS_Option> Options { get; }
        IDbSet<SYS_UserCompany> UsersCompany { get; }
        IDbSet<SYS_WorkSpace> WorkSpaces { get; }
        IDbSet<SYS_WorkSpaceType> WorkSpaceTypes { get; }
    }
}
