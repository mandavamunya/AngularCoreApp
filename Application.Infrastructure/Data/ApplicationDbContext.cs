using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<JournoRanking> JournoRankings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

    }
}
