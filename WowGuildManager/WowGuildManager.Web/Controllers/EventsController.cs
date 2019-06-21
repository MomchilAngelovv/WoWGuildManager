using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Events;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Dungeons;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.Controllers
{
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
        //TODO:REARANGE private and public stuff
        private bool IsCharacterRegisteredForDungeon(Character character, DungeonViewModel dungeon)
        {
            return character.Dungeons.Any(d => d.DungeonId == dungeon.Id);
        }

        public async Task<IActionResult> Index()
        {
            var userId = await this.GetUserId(this.userManager);

            var myCharacters = this.charactersService
                .GetCharactersByUserId<Character>(userId);

            var dungeons = this.dungeonService
                .GetAll<DungeonViewModel>()
                .ToList();

            var raids = this.raidService
               .GetAll<RaidViewModel>()
               .ToList();

            //TODO: THink if something eazier is possible
            //TODO: Consider Events view change border if joined or no
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
                //TODO: 10000000% REFACTOR THIS WITH MAPPE this make query for eveyr dungon
                //var joinedCharacter = this.charactersService
                //    .GetRegisteredCharacterForCurrentDungeon<CharacterNameRoleViewModel>(dungeon.Id, myCharacterIds);

                //if (joinedCharacter != null)
                //{
                //    dungeon.AlreadyJoined = true;
                //    dungeon.JoinedCharacter = joinedCharacter;
                //}
            }

            //TODO: Code queility when finish !!!!!! IMPORTNAT !!!
           
            var eventsIndexViewModel = new EventsIndexViewModel
            {
                IsUserRaidLeaderOrAdmin = this.User.IsInRole("Raid Leader") || this.User.IsInRole("Admin"),
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }

        private static void SetJoinedCharacterToDungeon(Character character, DungeonViewModel dungeon)
        {
            var joinedCharacter = new CharacterNameRoleViewModel
            {
                Name = character.Name,
                Role = character.Role.ToString()
            };

            dungeon.AlreadyJoined = true;
            dungeon.JoinedCharacter = joinedCharacter;
        }

        private static void SetJoinedCharacterToDungeon(Character character, RaidViewModel raid)
        {
            var joinedCharacter = new CharacterNameRoleViewModel
            {
                Name = character.Name,
                Role = character.Role.ToString()
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