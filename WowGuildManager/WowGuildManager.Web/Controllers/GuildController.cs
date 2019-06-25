using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ViewModels.Guild;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.Controllers
{
    public class GuildController : BaseController
    {
        private readonly IRaidService raidService;

        public GuildController(IRaidService raidService)
        {
            this.raidService = raidService;
        }

        public IActionResult Progress()
        {
            var raidDestinations = this.raidService.GetDestinations<RaidDestinationProgressViewModel>();

            var guildProgressViewModel = new GuildProgressViewModel
            {
                RaidDestinationProgresses = raidDestinations
            };

            return this.View(guildProgressViewModel);
        }
    }
}
