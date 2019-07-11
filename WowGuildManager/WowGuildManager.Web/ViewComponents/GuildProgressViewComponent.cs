using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ViewModels.Guild;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.ViewComponents
{
    public class GuildProgressViewComponent : ViewComponent
    {
        private readonly IRaidService raidService;

        public GuildProgressViewComponent(IRaidService raidService)
        {
            this.raidService = raidService;
        }

        public IViewComponentResult Invoke()
        {
            var raidDestinations = this.raidService.GetDestinations<RaidDestinationProgressViewModel>();

            return this.View(raidDestinations);
        }
    }
}
