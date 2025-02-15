
using CloudPOS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudPOS.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateAsync(RoleViewModel roleViewModel)
        {
            var existingRole = await _roleManager.RoleExistsAsync(roleViewModel.Name);
            if (!existingRole)
            {
                var role = new IdentityRole()
                {
                    Name = roleViewModel.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded) 
                {
                    throw new Exception("Role Create fail");
                }
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string Id)
        {
            var role =await _roleManager.FindByIdAsync(Id);
            if(role == null)
            {
                throw new Exception("Role Not Found");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Deleting Role fail");
            }
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var roleViewModels = roles.Select(r =>new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return await Task.FromResult(roleViewModels);
        }
    }
}
