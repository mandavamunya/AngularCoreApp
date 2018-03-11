﻿// <auto-generated />
using Application.Core.Enums;
using Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Application.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Application.Core.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PublishDate");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Application.Core.Entities.JournoRanking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<double>("AverageComments");

                    b.Property<double>("AverageViews");

                    b.Property<DateTime>("Date");

                    b.Property<int>("NumberOfPosts");

                    b.Property<int>("Ranking");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("JournoRanking");
                });

            modelBuilder.Entity("Application.Core.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("Articles");

                    b.Property<int>("BlogId");

                    b.Property<int>("Comments");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Application.Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("Role");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Team");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Application.Core.Entities.JournoRanking", b =>
                {
                    b.HasOne("Application.Infrastructure.Identity.ApplicationUser", "ApplicationUser")
                        .WithMany("JournoRankings")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Application.Core.Entities.Post", b =>
                {
                    b.HasOne("Application.Infrastructure.Identity.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Application.Core.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
