//TODO: LOG ERRORS
//TODO: appsettings.json for production
namespace WowGuildManager.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using WowGuildManager.Web.Extensions;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Data;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Services.Raids;
    using WowGuildManager.Services.Api;
    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Web.Filters.ActionFilters;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WowGuildManagerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WowGuildManagerDbContext"));
            });

            services.AddIdentity<WowGuildManagerUser, WowGuildManagerRole>(options =>
             {
                //TODO: Fix password settings
                options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 3;
                 options.Password.RequiredUniqueChars = 0;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
             })
            .AddEntityFrameworkStores<WowGuildManagerDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "440150153493696";
                options.AppSecret = "7b330adc49cae00c5b11d5e58e0caa77";
            });

            services.AddAutoMapper(typeof(Startup));
             
            services.AddMvc(options => 
            {
                options.Filters.Add(new ValidateModelStateActionFilter());
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })  
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region AppServices
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IDungeonService, DungeonService>();
            services.AddTransient<IRaidService, RaidService>();
            services.AddTransient<IApiService, ApiService>();
            services.AddTransient<IGuildService, GuildService>();
            #endregion 
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region AppMiddlewares
            app.UseSeedAdminUserAndRoles();
            app.UseSeedDatabaseDefaultData();
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "mvcAreaRoute",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
