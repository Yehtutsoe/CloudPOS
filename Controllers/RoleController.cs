using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IActionResult> Role()
        {
            var roles = await _roleService.GetAllRoles();
            ViewData["Roles"] = roles;
            return View(new RoleViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Role(RoleViewModel roleViewModel)
        {
            try
            {
                await _roleService.CreateAsync(roleViewModel);
                ViewData["Info"] = "Successfully create role";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Unsuccessully create role" + e.Message;
                ViewData["Status"] = false;
            }
            return View(new RoleViewModel()); 
        }

        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                await _roleService.DeleteAsync(Id);
                ViewData["Info"] = "Successfully Delete the  Role";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Unsuccessfully Delete the role" + e.Message;
                ViewData["Status"] = false;
            }
            return View(new RoleViewModel());  
        }
    }
}
