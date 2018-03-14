using Application.Core.Entities;
using Application.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
            UserManager<ApplicationUser> userManager,
            AppIdentityDbContext appIdentityDbContext)
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
                    Email = "johndoe@email.com",
                    Role = Role.Developer
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
                    Email = "test@email.com",
                    Role = Role.Journalist
                };
                await userManager.CreateAsync(user, "P@ssw0rd!");
                await userManager.AddToRoleAsync(user, "Journalist");
            }

            // Seed Blog
            var blogs = new List<Blog>()
            {
                new Blog { Name = "Insights", IsPublished = true, CreateDate = DateTime.Now, PublishDate = DateTime.Now },
                new Blog { Name = "Articles", IsPublished = true, CreateDate = DateTime.Now.AddDays(7), PublishDate = DateTime.Now.AddDays(7)},
                new Blog { Name = "News", IsPublished = true, CreateDate = DateTime.Now.AddDays(101), PublishDate = DateTime.Now.AddDays(101) },
                new Blog { Name = "Best Artciles", IsPublished = true, CreateDate = DateTime.Now.AddDays(30), PublishDate = DateTime.Now.AddDays(30) },
                new Blog { Name = "Featured Articles", IsPublished = true, CreateDate = DateTime.Now.AddDays(5), PublishDate = DateTime.Now.AddDays(5) }
            };

            foreach (var blog in blogs)
            {
                await appIdentityDbContext.Blogs.AddAsync(blog);
            }
            await appIdentityDbContext.SaveChangesAsync();
        }


    }
}
