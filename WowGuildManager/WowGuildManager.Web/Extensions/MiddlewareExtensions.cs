using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowGuildManager.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedAdminUserAndDefaultRoles(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SeedAdminUserAndDefaultRoles>();
        }
    }

}
