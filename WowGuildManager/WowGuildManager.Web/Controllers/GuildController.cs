namespace WowGuildManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Raids;
    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Models.ViewModels.Users;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Guild;
    using WowGuildManager.Models.BindingModels.Guilds;

    public class GuildController : BaseController
    {
        private readonly IRaidService raidService;
        private readonly IGuildService guildService;

        public GuildController(
            IRaidService raidService, 
            IGuildService guildService)
        {
            this.raidService = raidService;
            this.guildService = guildService;
        }

        [HttpGet]
        public IActionResult GuildMaster()
        {
            var registeredCharacters = this.guildService.GetTotalRegisteredCharactersCount();
            var users = this.guildService.GetTotalRegisteredUsers<UserGuildMasterViewModel>();
            var raidDestinations = this.raidService.GetDestinations<RaidDestinationGuildMasterProgressViewModel>();

            var guildMasterViewModel = new GuildMasterViewModel
            {
                RegisteredCharactersCount = registeredCharacters,
                Users = users,
                RaidDestinations = raidDestinations
            };

            return this.View(guildMasterViewModel);
        }
        [HttpGet]
        public IActionResult Progress()
        {
            var guildProgressViewModel = new GuildProgressViewModel
            {

            };

            return this.View(guildProgressViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AddGuildProgressAsync(string raidName)
        {
            await this.guildService.AddProgressToRaidAsync(raidName);
            return this.RedirectToAction(nameof(GuildMaster));
        }
        [HttpGet]
        public async Task<IActionResult> RemoveGuildProgressAsync(string raidName)
        {
            await this.guildService.RemoveProgressToRaidAsync(raidName);
            return this.RedirectToAction(nameof(GuildMaster));
        }
        [HttpGet]
        public async Task<IActionResult> PromoteRankAsync(string characterId)
        {
            await this.guildService.PromoteRankAsync(characterId);
            return this.RedirectToAction("Details", "Characters", new { id = characterId });
        }
        [HttpGet]
        public async Task<IActionResult> DemoteRankAsync(string characterId)
        {
            await this.guildService.DemoteRankAsync(characterId);
            return this.RedirectToAction("Details", "Characters", new { id = characterId });
        }
        [HttpGet]
        public async Task<IActionResult> SetOrUnsetRaidLeaderAsync(string userId)
        {
            await this.guildService.SetOrUnsetRaidLeaderAsync(userId);
            return RedirectToAction(nameof(GuildMaster));
        }
    }
}
