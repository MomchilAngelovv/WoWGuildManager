//TODO: MAKE RESPONSIVE VERY BAD
namespace WowGuildManager.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using WowGuildManager.Services.Raids;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Events;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;

    [Authorize]
    public class EventsController : BaseController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IDungeonService dungeonService;
        private readonly IRaidService raidService;
        private readonly ICharacterService charactersService;

        public EventsController(
            UserManager<WowGuildManagerUser> userManager,
            IDungeonService dungeonService,
            IRaidService raidService,
            ICharacterService charactersService)
        {
            this.userManager = userManager;
            this.dungeonService = dungeonService;
            this.raidService = raidService;
            this.charactersService = charactersService;
        }

        [HttpGet]
        public IActionResult History()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult Today()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult Upcoming()
        {
            var userId = this.userManager.GetUserId(this.User);

            var myCharacters = this.charactersService
                .GetUserCharacters<Character>(userId);

            var dungeons = this.dungeonService
                .GetAllUpcoming<DungeonViewModel>()
                .ToList();

            var raids = this.raidService
               .GetAllUpcoming<RaidViewModel>()
               .ToList();

            foreach (var character in myCharacters)
            {
                foreach (var dungeon in dungeons)
                {
                    if (IsCharacterRegisteredForDungeon(character, dungeon))
                    {
                        SetJoinedCharacterToDungeon(character, dungeon);
                    }
                }

                foreach (var raid in raids)
                {
                    if (IsCharacterRegisteredForRaid(character, raid))
                    {
                        SetJoinedCharacterToRaid(character, raid);
                    }
                }
            }
           
            var eventsIndexViewModel = new EventsIndexViewModel
            {
                IsUserRaidLeaderOrGuildMaster = this.User.IsInRole("GuildMaster") || this.User.IsInRole("RaidLeader"),
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }

        private bool IsCharacterRegisteredForRaid(Character character, RaidViewModel raid)
        {
            return character.Raids.Any(r => r.RaidId == raid.Id);
        }
        private bool IsCharacterRegisteredForDungeon(Character character, DungeonViewModel dungeon)
        {
            return character.Dungeons.Any(dung => dung.DungeonId == dungeon.Id);
        }
        private static void SetJoinedCharacterToDungeon(Character character, DungeonViewModel dungeon)
        {
            var joinedCharacter = new CharacterNameRoleViewModel
            {
                Name = character.Name,
                Role = character.Role.Name
            };

            dungeon.AlreadyJoined = true;
            dungeon.JoinedCharacter = joinedCharacter;
        }
        private static void SetJoinedCharacterToRaid(Character character, RaidViewModel raid)
        {
            var joinedCharacter = new CharacterNameRoleViewModel
            {
                Name = character.Name,
                Role = character.Role.Name
            };

            raid.AlreadyJoined = true;
            raid.JoinedCharacter = joinedCharacter;
        }
    }
}