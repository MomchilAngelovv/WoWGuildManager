using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ViewModels.Admin;
using WowGuildManager.Models.ViewModels.Users;
using WowGuildManager.Services.Guilds;

namespace WowGuildManager.Web.ViewComponents
{
    public class SelectGuildMasterViewComponent : ViewComponent
    {
        private readonly IGuildService guildService;

        public SelectGuildMasterViewComponent(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        public IViewComponentResult Invoke()
        {
            var users = this.guildService.GetRegisteredUsers<UserAdminViewModel>();

            return this.View(users);
        }
    }
}
