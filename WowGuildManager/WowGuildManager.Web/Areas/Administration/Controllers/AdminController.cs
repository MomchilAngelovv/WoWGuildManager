namespace WowGuildManager.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using WowGuildManager.Models.ViewModels.Admin;
    using WowGuildManager.Models.ViewModels.Users;
    using WowGuildManager.Services.Guilds;
    using Microsoft.AspNetCore.Identity;
    using WowGuildManager.Domain.Identity;
    using System.Linq;

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
            var registeredUsers = this.guildService.GetRegisteredUsersCount();
            var registeredCharactersCount = this.guildService.GetRegisteredCharactersCount();

            var adminIndexViewModel = new AdminIndexViewModel
            {
                RegisteredUsers = registeredUsers,
                RegisteredCharacters = registeredCharactersCount
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