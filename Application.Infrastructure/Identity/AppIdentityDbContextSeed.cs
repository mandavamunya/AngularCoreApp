using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppIdentityDbContextSeed(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe@email.com",
                Email = "johndoe@email.com"
            };

            await userManager.CreateAsync(defaultUser, "P@ssw0rd!");

        }

        public async Task CreateRoles()
        {
            string[] roles = { "Developer", "Administrator", "Manager", "Journalist", "Editor" };
            IdentityResult roleResult;

            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                }

                var superuser = new ApplicationUser
                {
                    //Email = _config.GetSection("UserSettings")["UserEmail"],
                    //UserName = _config.GetSection("UserSettings")["UserEmail"]
                };

                //string password = _config.GetSection("UserSettings")["UserPassword"];

                var user = await _userManager.FindByEmailAsync(superuser.Email);
                if (user == null)
                {
                    //var createUser = await _userManager.CreateAsync(superuser, password);
                    //if (createUser != null)
                    //{
                    //    await _userManager.AddToRoleAsync(superuser, "Developer");
                    //}
                }

            }
        }
    }
}
