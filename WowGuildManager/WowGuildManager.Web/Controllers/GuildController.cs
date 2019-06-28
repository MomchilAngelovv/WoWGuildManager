using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ViewModels.Guild;
using WowGuildManager.Services.Guilds;
using WowGuildManager.Services.Raids;

//TOD0: FIX ALL EXLUDE DEVELOPMENT LIBRATIES !~~
namespace WowGuildManager.Web.Controllers
{
    public class GuildController : BaseController
    {
        private readonly IRaidService raidService;
        private readonly IGuildService guildService;

        public GuildController(IRaidService raidService, IGuildService guildService)
        {
            this.raidService = raidService;
            this.guildService = guildService;
        }

        public IActionResult Index()
        {
            return this.View();
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

        public async Task<IActionResult> AddProgress(string raidName)
        {
            await this.guildService.AddProgressToRaid(raidName);
            return this.RedirectToAction(nameof(Progress));
        }

        public async Task<IActionResult> RemoveProgress(string raidName)
        {
            await this.guildService.RemoveProgressToRaid(raidName);
            return this.RedirectToAction(nameof(Progress));
        }
    }
}
