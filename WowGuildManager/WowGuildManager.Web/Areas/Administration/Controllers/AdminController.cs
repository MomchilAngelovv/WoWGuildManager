namespace WowGuildManager.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Models.ViewModels.Admin;

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
            var registeredUsersCount = this.guildService.GetRegisteredUsersCount();
            var registeredCharactersCount = this.guildService.GetRegisteredCharactersCount();

            var adminIndexViewModel = new AdminIndexViewModel
            {
                RegisteredUsersCount = registeredUsersCount,
                RegisteredCharactersCount = registeredCharactersCount
            };

            return this.View(adminIndexViewModel); 
        }

        public async Task<IActionResult> SetGuildMaster(string id)
        {
            await this.guildService.SetGuildMasterAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}