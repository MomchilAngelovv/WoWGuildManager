using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowGuildManager.Web.Extensions
{
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
