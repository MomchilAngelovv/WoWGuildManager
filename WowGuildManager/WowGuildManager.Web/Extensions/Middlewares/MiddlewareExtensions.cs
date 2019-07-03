namespace WowGuildManager.Web.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedAdminUserAndRoles(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SeedAdminUserAndRoles>();
        }

        public static IApplicationBuilder UseSeedDatabaseDefaultData(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SeedDatabaseDefaultData>();
        }
    }
}
