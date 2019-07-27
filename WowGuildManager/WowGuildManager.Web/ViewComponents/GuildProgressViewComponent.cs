namespace WowGuildManager.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Raids;
    using WowGuildManager.Models.ViewModels.Guild;

    public class GuildProgressViewComponent : ViewComponent
    {
        private readonly IRaidService raidService;

        public GuildProgressViewComponent(
            IRaidService raidService)
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
