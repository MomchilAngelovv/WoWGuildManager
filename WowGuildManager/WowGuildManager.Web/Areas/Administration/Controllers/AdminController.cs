namespace WowGuildManager.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Models.ViewModels.Admin;
    using WowGuildManager.Models.ViewModels.Users;
    using WowGuildManager.Services.Guilds;
    //TODO: Put bootstrap icons
    [Area("Administration")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IGuildService guildService;

        public AdminController(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        public IActionResult Index()
        {
            var users = this.guildService.GetRegisteredUsers<UserAdminViewModel>();

            var adminIndexViewModel = new AdminIndexViewModel
            {
                Users = users
            };

            return this.View(adminIndexViewModel); ;
        }

        public async Task<IActionResult> SetGuildMaster(string id)
        {
            await this.guildService.SetGuildMasterAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}