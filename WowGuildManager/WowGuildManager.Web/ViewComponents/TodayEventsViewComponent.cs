namespace WowGuildManager.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Raids;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Events;
    using WowGuildManager.Models.ViewModels.Dungeons;

    public class TodayEventsViewComponent : ViewComponent
    {
        private readonly IDungeonService dungeonService;
        private readonly IRaidService raidService;

        public TodayEventsViewComponent(
            IDungeonService dungeonService, 
            IRaidService raidService)
        {
            this.dungeonService = dungeonService;
            this.raidService = raidService;
        }

        public IViewComponentResult Invoke()
        {
            var raidsForToday = this.raidService
               .GetTodayRaids<RaidIdDestinationViewModel>();

            var dungeonsForToday = this.dungeonService
                .GetTodayDungeons<DungeonIdDestinationViewModel>();

            var todayEventsViewModel = new TodayEventsViewModel
            {
                Raids = raidsForToday,
                Dungeons = dungeonsForToday
            };

            return this.View(todayEventsViewModel);
        }
    }
}
