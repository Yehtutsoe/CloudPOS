using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IRoleService
    {
        Task CreateAsync(RoleViewModel roleViewModel);
        Task<IEnumerable<RoleViewModel>> GetAllRoles();
        Task DeleteAsync(string Id);

    }
}
