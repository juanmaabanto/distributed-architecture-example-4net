using CatSolution.CrossCutting.Security.AuthenticationAPI.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public int WorkSpaceId { get; set; }

        public virtual SYS_WorkSpace SYS_WorkSpace { get; set; }
    }
}