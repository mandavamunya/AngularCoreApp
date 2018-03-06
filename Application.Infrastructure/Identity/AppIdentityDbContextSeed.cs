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

        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {

            // Create Roles
            string[] roles = { "Developer", "Administrator", "Manager", "Journalist", "Editor" };
            IdentityResult roleResult;
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create Default Super User
            if (await userManager.FindByEmailAsync("johndoe@email.com") == null)
            {
                var defaultUser = new ApplicationUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "johndoe@email.com",
                    Email = "johndoe@email.com"
                };

                await userManager.CreateAsync(defaultUser, "P@ssw0rd!");
                await userManager.AddToRoleAsync(defaultUser, "Developer");
            }

            // Create Another Dummy User
            if (await userManager.FindByEmailAsync("test@email.com") == null)
            {
                var user = new ApplicationUser()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    UserName = "test@email.com",
                    Email = "test@email.com"
                };
                await userManager.CreateAsync(user, "P@ssw0rd!");
                await userManager.AddToRoleAsync(user, "Journalist");
            }
        }


    }
}
