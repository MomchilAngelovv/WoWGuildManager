namespace WowGuildManager.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Events;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Services.Raids;

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
