using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Web.Extensions
{
    public class SeedAdminUserAndRoles
    {
        private readonly RequestDelegate _next;

        public SeedAdminUserAndRoles(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<WowGuildManagerUser> userManager,
            RoleManager<WowGuildManagerRole> roleManager)
        {
            if (roleManager.Roles.Any() == false)
            {
                await SeedRoles(userManager, roleManager);
            }

            await this._next(context);
        }

        private async Task SeedRoles(
             UserManager<WowGuildManagerUser> userManager,
             RoleManager<WowGuildManagerRole> roleManager)
        {
            var roleNamesAndDescriptions = new Dictionary<string, string>
            {
                [WowGuildManagerUserConstants.Admin] = WowGuildManagerUserConstants.AdminDescription,
                [WowGuildManagerUserConstants.RaidLeader] = WowGuildManagerUserConstants.RaidLeaderDescription,
                [WowGuildManagerUserConstants.DefaultUser] = WowGuildManagerUserConstants.DefaultUserDescription
            };

            foreach (var roleNameAndDescription in roleNamesAndDescriptions)
            {
                var role = new WowGuildManagerRole
                {
                    Name = roleNameAndDescription.Key,
                    Description = roleNameAndDescription.Value
                };

                await roleManager.CreateAsync(role);
            }
          
            var adminUser = new WowGuildManagerUser
            {
                Email = WowGuildManagerUserConstants.AdminEmail,
                UserName = WowGuildManagerUserConstants.AdminUserName,
            };

            //TODO: Fix password and admin info
            await userManager.CreateAsync(adminUser, WowGuildManagerUserConstants.AdminPassword);
            await userManager.AddToRoleAsync(adminUser, WowGuildManagerUserConstants.Admin);
        }
    }
}
