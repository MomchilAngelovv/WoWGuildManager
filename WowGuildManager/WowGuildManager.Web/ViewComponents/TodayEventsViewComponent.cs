using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Events;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Dungeons;
using WowGuildManager.Services.Raids;

//TODO: Considere to replace everywhere with async methods when possible
namespace WowGuildManager.Web.ViewComponents
{
    public class TodayEventsViewComponent : ViewComponent
    {
        private readonly IDungeonService dungeonService;
        private readonly IRaidService raidService;

        public TodayEventsViewComponent(IDungeonService dungeonService, IRaidService raidService)
        {
            this.dungeonService = dungeonService;
            this.raidService = raidService;
        }

        public IViewComponentResult Invoke()
        {
            var raidsForToday = this.raidService
               .GetRaidsForToday<RaidIdDestinationViewModel>();

            var dungeonsForToday = this.dungeonService
                .GetDungeonsForToday<DungeonIdDestinationViewModel>();

            var todayEventsViewModel = new TodayEventsViewModel
            {
                Raids = raidsForToday,
                Dungeons = dungeonsForToday
            };

            return this.View(todayEventsViewModel);
        }
    }
}
