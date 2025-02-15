using CloudPOS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudPOS.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this._signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Create()
        {
            var roles = _roleManager.Roles.Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                };
                var result = await _userManager.CreateAsync(user,userViewModel.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(userViewModel.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, userViewModel.SelectedRole);    

                    }
                    TempData["Message"] = "Successfully create user";
                    return RedirectToAction("List");
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["ErrorMessage"] = "User creation failed. Please don't space your name and check already exist and your password must be strong!";
            }
            var role = _roleManager.Roles.Select(r =>new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            ViewBag.Roles = role;
            return View();
        }
        public IActionResult List()
        {
            var users = _userManager.Users.Select(user => new
            {
                User = user,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();
            var model = users.Select(m => new UserListViewModel
            {
                UserId = m.User.Id,
                UserName = m.User.UserName,
                Email = m.User.Email,
                Roles = string.Join(",",m.Roles)
            }).ToList();
            return View(model);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                TempData["ErrorMessage"] = "Not found user";
                return RedirectToAction(nameof(List));
            }
            var user =await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction(nameof(List));
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User is Deleted";
            }
            else
            {
                TempData["ErrorMessage"] = "Error for user deleting";
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Invalid user ID.";
                return RedirectToAction("UserList");
            }


            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("UserList");
            }


            var userRoles = await _userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault();

            var model = new UserListViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = currentRole
            };

            ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return RedirectToAction("List");
                }
                user.UserName = model.UserName;
                user.Email = model.Email;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToList();

                    return View("EditUser", model);
                }


                var currentRoles = await _userManager.GetRolesAsync(user);
                if (!string.IsNullOrEmpty(model.Roles) && !currentRoles.Contains(model.Roles))
                {
                    if (currentRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }
                    await _userManager.AddToRoleAsync(user, model.Roles);
                }

                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("List");
            }

            ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View("EditUser", model);
        }
    }
}
