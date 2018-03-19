using Application.Core.Entities;
using Application.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
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
                    PhoneNumber = "0717367946",
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
                    PhoneNumber = "0632784635",
                    Role = Role.Journalist
                };
                await userManager.CreateAsync(user, "P@ssw0rd!");
                await userManager.AddToRoleAsync(user, "Journalist");
            }

            // Seed Blog

            var blog1 = new Blog {
                Name = "Insights",
                PostType = PostType.Insight,
                IsPublished = true,
                Posts = new List<Post>()
                    {
                        new Post
                        {
                            Content = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful.",
                            Description = "this is an insight",
                            Publications = 1001,
                            Comments = 50000,
                            Type = PostType.Insight,
                            BlogId = 1,
                            CreateDate = DateTime.Now.AddDays(1),
                            PublishDate = DateTime.Now.AddDays(3)
                        },
                        new Post
                        {
                            Content = "Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus.",
                            Description = "insight description",
                            Publications = 5362,
                            Comments = 7647,
                            Type = PostType.Insight,
                            BlogId = 1,
                            CreateDate = DateTime.Now.AddDays(4),
                            PublishDate = DateTime.Now.AddDays(5)
                        }
                    },
                CreateDate = DateTime.Now,
                PublishDate = DateTime.Now
            };
            appIdentityDbContext.Blogs.Add(blog1);
            appIdentityDbContext.Posts.AddRange(blog1.Posts);

            var blog2 = new Blog {
                Name = "Articles",
                PostType = PostType.Article,
                IsPublished = true,
                Posts = new List<Post>()
                    {
                        new Post
                        {
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborumt",
                            Description = "some article 1 description",
                            Publications = 50,
                            Comments = 10000,
                            Type = PostType.Article,
                            BlogId = 2,
                            CreateDate = DateTime.Now,
                            PublishDate = DateTime.Now.AddDays(2)
                        },
                        new Post
                        {
                            Content = "The lorem ipsum text is typically a scrambled section of De finibus bonorum et malorum, a 1st-century BC Latin text by Cicero, with words altered, added, and removed to make it nonsensical, improper Latin.",
                            Description = "some article 2 description",
                            Publications = 1280,
                            Comments = 1785,
                            Type = PostType.Article,
                            BlogId = 2,
                            CreateDate = DateTime.Now.AddHours(3),
                            PublishDate = DateTime.Now.AddHours(7),
                        }
                    },
                CreateDate = DateTime.Now.AddDays(7),
                PublishDate = DateTime.Now.AddDays(7)
            };
            appIdentityDbContext.Blogs.Add(blog2);
            appIdentityDbContext.Posts.AddRange(blog2.Posts);

            var blog3 = new Blog
            {
                Name = "News",
                PostType = PostType.News,
                IsPublished = true,
                Posts = new List<Post>()
                    {
                        new Post
                        {
                            Content = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet.",
                            Description = "John Doe goes missing",
                            Publications = 78,
                            Comments = 30000,
                            Type = PostType.News,
                            BlogId = 3,
                            CreateDate = DateTime.Now.AddDays(8),
                            PublishDate = DateTime.Now.AddDays(15)
                        },
                        new Post
                        {
                            Content = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo.",
                            Description = "Description",
                            Publications = 2478,
                            Comments = 7663,
                            Type = PostType.News,
                            BlogId = 3,
                            CreateDate = DateTime.Now.AddHours(1),
                            PublishDate = DateTime.Now.AddDays(5)
                        },
                        new Post
                        {
                            Content = "A lorem ipsum placeholder text was inadvertently published in The Straits Times, a Singapore newspaper, on 26 April 2014. Occasionally used by printers as dummy texts since the 1500s, use of Lorem ipsum passages was first popularized on Letraset dry-transfer sheets from the early 1970s, which were produced to be used by graphic designers for filler text.",
                            Description = "news description",
                            Publications = 3278,
                            Comments = 9663,
                            Type = PostType.News,
                            BlogId = 3,
                            CreateDate = DateTime.Now.AddHours(3),
                            PublishDate = DateTime.Now.AddDays(7)
                        }
                    },
                CreateDate = DateTime.Now.AddDays(101),
                PublishDate = DateTime.Now.AddDays(101)
            };
            appIdentityDbContext.Blogs.Add(blog3);
            appIdentityDbContext.Posts.AddRange(blog3.Posts);

            var blog4 = new Blog
            {
                Name = "Featured Articles",
                PostType = PostType.FeaturedArticle,
                IsPublished = true,
                Posts = new List<Post>()
                    {
                        new Post
                        {
                            Content = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
                            Description = "this is a featured article",
                            Publications = 17,
                            Comments = 17000,
                            Type = PostType.FeaturedArticle,
                            BlogId = 4,
                            CreateDate = DateTime.Now.AddDays(6),
                            PublishDate = DateTime.Now.AddDays(7)
                        },
                        new Post
                        {
                            Content = "At vero eos et accusamus et iusto odio dignissimos ducimus, qui blanditiis praesentium voluptatum deleniti atque corrupti, quos dolores et quas molestias excepturi sint, obcaecati cupiditate non-provident, similique sunt in culpa, qui officia deserunt mollitia animi, id est laborum et dolorum fuga.",
                            Description = "this is a featured article 2",
                            Publications = 1677,
                            Comments = 6370,
                            Type = PostType.FeaturedArticle,
                            BlogId = 4,
                            CreateDate = DateTime.Now.AddDays(6),
                            PublishDate = DateTime.Now.AddDays(10)
                        }
                    },
                CreateDate = DateTime.Now.AddDays(30),
                PublishDate = DateTime.Now.AddDays(30)
            };
            appIdentityDbContext.Blogs.Add(blog4);
            appIdentityDbContext.Posts.AddRange(blog4.Posts);

            var blog5 = new Blog
            {
                Name = "Featured News",
                PostType = PostType.FeaturedNews,
                IsPublished = true,
                Posts = new List<Post>()
                    {
                        new Post
                        {
                            Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem",
                            Description = "this is a featured news articles",
                            Publications = 4673,
                            Comments = 9647,
                            Type = PostType.FeaturedNews,
                            BlogId = 5,
                            CreateDate = DateTime.Now.AddDays(21),
                            PublishDate = DateTime.Now.AddDays(23)
                        },
                        new Post
                        {
                            Content = "In dui magna, posuere eget, vestibulum et, tempor auctor, justo. In ac felis quis tortor malesuada pretium. Pellentesque auctor neque nec urna. Proin sapien ipsum, porta a, auctor quis, euismod ut, mi. Aenean viverra rhoncus pede.",
                            Description = "featured news description",
                            Publications = 3664,
                            Comments = 1647,
                            Type = PostType.FeaturedNews,
                            BlogId = 5,
                            CreateDate = DateTime.Now.AddDays(21),
                            PublishDate = DateTime.Now.AddDays(24)
                        }
                    },
                CreateDate = DateTime.Now.AddDays(5),
                PublishDate = DateTime.Now.AddDays(5)
            };
            appIdentityDbContext.Blogs.Add(blog5);
            appIdentityDbContext.Posts.AddRange(blog5.Posts);


            // Seed Blog Category: NotSpecified, Insight, Article, News, FeaturedArticle, FeaturedNews
            var categories = new List<BlogCategory>()
            {
                new BlogCategory { Name = "Insights", Type = PostType.Insight },
                new BlogCategory { Name = "Articles", Type = PostType.Article },
                new BlogCategory { Name = "News", Type = PostType.News },
                new BlogCategory { Name = "FeaturedArticle", Type = PostType.FeaturedArticle },
                new BlogCategory { Name = "FeaturedNews", Type = PostType.FeaturedNews }
            };


            foreach (var category in categories)
            {
                await appIdentityDbContext.BlogCategories.AddAsync(category);
            }

            await appIdentityDbContext.SaveChangesAsync();

        }


    }
}
