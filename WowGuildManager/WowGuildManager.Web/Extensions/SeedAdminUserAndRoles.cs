﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Web.Extensions
{
    public class SeedAdminUserAndRoles
    {
        private readonly RequestDelegate _next;

        public SeedAdminUserAndRoles(
            RequestDelegate next)
        {
            _next = next;
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

            await _next(context);
        }

        private async Task SeedRoles(
             UserManager<WowGuildManagerUser> userManager,
             RoleManager<WowGuildManagerRole> roleManager)
        {
            await roleManager.CreateAsync(new WowGuildManagerRole
            {
                Name = "Admin",
                Description = "Full access."
            });

            await roleManager.CreateAsync(new WowGuildManagerRole
            {
                Name = "RaidLeader",
                Description = "Authorized to create raid events."
            });

            await roleManager.CreateAsync(new WowGuildManagerRole
            {
                Name = "DefaultUser",
                Description = "Default user."
            });

            var adminUser = new WowGuildManagerUser
            {
                Email = "Admin@admin.com",
                UserName = "Monkata",
            };

            //TODO: Fix password
            string pass = "123";

            await userManager.CreateAsync(adminUser, pass);

            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}