using CatSolution.Domain.MainModule.Contracts.Users;
using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Domain.MainModule.Services
{
    public class UserService : IUserService
    {
        public void AssignCompany(AspNetUser user, byte companyId, bool principal)
        {
            user.SYS_UserCompany.Add(
                new SYS_UserCompany() {
                    CompanyId = companyId,
                    Principal = principal
                });
        }
    }
}
