using System;
using System.Collections.Generic;
using System.IO;
using Application.Core.Entities;
using Application.Core.Interfaces;
using Application.Infrastructure.Data;
using Application.Infrastructure.Identity;
using Application.Infrastructure.Logging;
using Application.Infrastructure.Services;
using Application.Models.PublicationViewModel;
using Application.Models.UserViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Application.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureProductionServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // Add Business Logic DbContext
            services.AddDbContext<ApplicationDbContext>(c =>
            {
                try
                {
                    c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            });

            // Add Identity DbContext
            var identityConnection = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(identityConnection));

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var autoConfig = new MapperConfiguration(config => {
                config.CreateMap<ApplicationUserViewModel, ApplicationUser>().ReverseMap();
                config.CreateMap<List<ApplicationUserViewModel>, List<ApplicationUser>>().ReverseMap();
                config.CreateMap<JournoRanking, JournoRankingViewModel>().ReverseMap();
                config.CreateMap<List<JournoRanking>, List<JournoRankingViewModel>>().ReverseMap();

            });

            env.WebRootPath =
                    Path.Combine(
                    Directory.GetCurrentDirectory(),
                    @"wwwroot/Files");
            if (!Directory.Exists(env.WebRootPath))
            {
                DirectoryInfo directoryInfo =
                    Directory.CreateDirectory(env.WebRootPath);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Files")),
                RequestPath = new PathString("/files")
            });

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Browser")),
            //    RequestPath = new PathString("/browser")
            //});

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
