﻿//TODO: MAKE RESPONSIVE VERY BAD
//TODO:REARANGE private and public stuff AT END !! IMPORTANT


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
        private bool IsCharacterRegisteredForDungeon(Character character, DungeonViewModel dungeon)
        {
            return character.Dungeons.Any(d => d.DungeonId == dungeon.Id);
        }

        public IActionResult Upcoming()
        {
            var userId = this.userManager.GetUserId(this.User);

            var myCharacters = this.charactersService
                .GetCharactersByUserId<Character>(userId);

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
                        SetJoinedCharacterToDungeon(character, raid);
                    }
                }
            }

            //TODO: Code queility when finish !!!!!! IMPORTNAT !!!
           
            var eventsIndexViewModel = new EventsIndexViewModel
            {
                IsUserRaidLeaderOrGuildMaster = this.User.IsInRole("GuildMaster") || this.User.IsInRole("RaidLeader"),
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }

        public IActionResult History()
        {
            return this.View();
        }

        public IActionResult Today()
        {
            return this.View();
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

        private static void SetJoinedCharacterToDungeon(Character character, RaidViewModel raid)
        { 
            var joinedCharacter = new CharacterNameRoleViewModel
            {
                Name = character.Name,
                Role = character.Role.Name
            };

            raid.AlreadyJoined = true;
            raid.JoinedCharacter = joinedCharacter;
        }

        private bool IsCharacterRegisteredForRaid(Character character, RaidViewModel raid)
        {
            return character.Raids.Any(d => d.RaidId == raid.Id);
        }
    }
}