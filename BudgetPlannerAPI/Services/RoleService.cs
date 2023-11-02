using Common.Exceptions.Base;

using Microsoft.AspNetCore.Identity;

using Services.Contracts;

namespace Services
{
    public class RoleService : IRoleService
    {
        public RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleService(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            var existingRole = await GetByName(roleName);

            if (existingRole is not null)
            {
                throw new BadRequestException($"Role: {roleName} already exists");
            }

            return await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }

        public async void DeleteRole(Guid roleId)
        {
            var role = await GetById(roleId) ?? throw new BadRequestException($"No role with Id {roleId}");
            await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityRole<Guid>?> GetById(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityRole<Guid>?> GetByName(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}
