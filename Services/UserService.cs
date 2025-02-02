
using Microsoft.AspNetCore.Identity;

namespace CloudPOS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> CreateUser(string UserName, string Email)
        {
            try
            {
                var user = CreateUser();
                user.UserName = Email;
                user.Email = Email;
                user.NormalizedUserName = UserName;
                user.NormalizedEmail = Email;
                var result = await _userManager.CreateAsync(user, "Speci@l92"); //create the user with default password
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee"); // create the role with employee
                }
                return user.Id;
            }
            catch
            {

                return string.Empty;
            }
        }
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {

                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'" + $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
