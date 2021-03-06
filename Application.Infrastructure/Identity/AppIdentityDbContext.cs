﻿using Application.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<JournoRanking> JournoRankings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Blog>()
                .HasIndex(o => new { o.PostType })
                .IsUnique();

            builder.Entity<BlogCategory>()
                .HasIndex(o => new { o.Type })
                .IsUnique();

        }
    }
}
