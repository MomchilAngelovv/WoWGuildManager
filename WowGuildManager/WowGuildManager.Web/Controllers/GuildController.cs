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

        public IActionResult GuildMaster()
        {
            var registeredCharacters = this.guildService.GetRegisteredCharactersCount();
            var users = this.guildService.GetRegisteredUsers<UserGuildMasterViewModel>();
            var raidDestinations = this.raidService.GetDestinations<RaidDestinationGuildMasterProgressViewModel>();

            var guildMasterViewModel = new GuildMasterViewModel
            {
                RegisteredCharactersCount = registeredCharacters,
                Users = users,
                RaidDestinations = raidDestinations
            };

            return this.View(guildMasterViewModel);
        }

        public IActionResult Progress()
        {
            var guildProgressViewModel = new GuildProgressViewModel
            {

            };

            return this.View(guildProgressViewModel);
        }

        public async Task<IActionResult> AddProgress(string raidName)
        {
            await this.guildService.AddProgressToRaid(raidName);
            return this.RedirectToAction(nameof(GuildMaster));
        }

        public async Task<IActionResult> RemoveProgress(string raidName)
        {
            await this.guildService.RemoveProgressToRaid(raidName);
            return this.RedirectToAction(nameof(GuildMaster));
        }

        public async Task<IActionResult> PromoteRank(string characterId)
        {
            await this.guildService.PromoteRankAsync(characterId);
            return this.RedirectToAction("Details", "Characters", new { id = characterId });
        }

        public async Task<IActionResult> DemoteRank(string characterId)
        {
            await this.guildService.DemoteRankAsync(characterId);
            return this.RedirectToAction("Details", "Characters", new { id = characterId });
        }

        public async Task<IActionResult> SetOrUnsetRaidLeader(string userId)
        {
            await this.guildService.SetOrUnsetRaidLeader(userId);

            return RedirectToAction(nameof(GuildMaster));
        }
    }
}
