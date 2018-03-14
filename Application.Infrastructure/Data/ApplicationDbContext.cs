using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

    }
}
