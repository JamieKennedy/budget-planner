using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRole(string roleName);
        void DeleteRole(Guid roldId);
        Task<IdentityRole<Guid>?> GetByName(string roleName);
        Task<IdentityRole<Guid>?> GetById(Guid id);

    }
}
