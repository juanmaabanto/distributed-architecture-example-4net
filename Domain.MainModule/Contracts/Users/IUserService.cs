using CatSolution.Domain.MainModule.Entities;

namespace CatSolution.Domain.MainModule.Contracts.Users
{
    public interface IUserService
    {
        void AssignCompany(AspNetUser user, byte companyId, bool principal);
    }
}
