using CatSolution.CrossCutting.Security.AuthenticationAPI.Entities;
using CatSolution.CrossCutting.Security.AuthenticationAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<SYS_Application> Applications { get; set; }
        public DbSet<SYS_Authorization> Authorizations { get; set; }
        public DbSet<SYS_ClientApp> Clients { get; set; }
        public DbSet<SYS_Company> Companies { get; set; }
        public DbSet<SYS_DetailAuthorization> DetailAuthorizations { get; set; }
        public DbSet<SYS_DetailOption> DetailOptions { get; set; }
        public DbSet<SYS_DetailWorkSpace> DetailWorkSpaces { get; set; }
        public DbSet<SYS_Option> Options { get; set; }
        public DbSet<SYS_RefreshToken> RefreshTokens { get; set; }
        public DbSet<SYS_UserCompany> UserCompanies { get; set; }
        public DbSet<SYS_WorkSpace> WorkSpaces { get; set; }
        public DbSet<SYS_WorkSpaceType> WorkSpacesTypes { get; set; }
        
        
    }
}