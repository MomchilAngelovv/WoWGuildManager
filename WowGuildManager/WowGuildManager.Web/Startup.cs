namespace WowGuildManager.Web
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using CloudinaryDotNet;

    using WowGuildManager.Data;
    using WowGuildManager.Services.Api;
    using WowGuildManager.Services.Raids;
    using WowGuildManager.Web.Extensions;
    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Gallery;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Web.Filters.ActionFilters;
    using WowGuildManager.Web.Filters.ExceptionFilters;

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
                options.UseLazyLoadingProxies();
            });

            services.AddIdentity<WowGuildManagerUser, WowGuildManagerRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<WowGuildManagerDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddResponseCompression();

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = this.Configuration["Facebook:AppId"];
                options.AppSecret = this.Configuration["Facebook:AppSecret"];
                options.CallbackPath = new PathString("/.auth/login/facebook/callback");
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidateModelStateActionFilter>();
                options.Filters.Add<LogErorInDatabaseExceptionFilter>();
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region AppServices
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IDungeonService, DungeonService>();
            services.AddTransient<IRaidService, RaidService>();
            services.AddTransient<IApiService, ApiService>();
            services.AddTransient<IGuildService, GuildService>();
            services.AddTransient<IGalleryService, GalleryService>();

            var cloud = this.Configuration["Cloudinary:Cloud"];
            var apiKey = this.Configuration["Cloudinary:ApiKey"];
            var apiSecret = this.Configuration["Cloudinary:ApiSecret"];
            var account = new Account(cloud, apiKey, apiSecret);
            var cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            #region AppMiddlewares
            app.UseSeedAdminUserAndRoles();
            app.UseSeedDatabaseDefaultData();
            #endregion

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseFileServer();
            StaticFileOptions option = new StaticFileOptions();
            FileExtensionContentTypeProvider contentTypeProvider = (FileExtensionContentTypeProvider)option.ContentTypeProvider ??
            new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings.Add(".unityweb", "application/octet-stream");
            option.ContentTypeProvider = contentTypeProvider;
            app.UseStaticFiles(option);
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
