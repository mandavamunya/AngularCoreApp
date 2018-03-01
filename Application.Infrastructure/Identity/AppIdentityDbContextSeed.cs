using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "johndoe@email.com", Email = "johndoe@email.com" };
            await userManager.CreateAsync(defaultUser, "P@ssw0rd!");
        }
    }
}