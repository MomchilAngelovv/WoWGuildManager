namespace WowGuildManager.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Models.ViewModels.Users;

    public class SelectGuildMasterViewComponent : ViewComponent
    {
        private readonly IGuildService guildService;

        public SelectGuildMasterViewComponent(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        public IViewComponentResult Invoke()
        {
            var users = this.guildService.GetTotalRegisteredUsers<UserAdminViewModel>();

            return this.View(users);
        }
    }
}
