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

        public async Task<IActionResult> Index()
        {
            var userId = await this.GetUserId(this.userManager);

            var dungeons = this.dungeonService
                .GetAll<DungeonViewModel>()
                .ToList();

            //TODO: 10000000% REFACTOR THIS WITH MAPPER

            foreach (var dungeon in dungeons)
            {
                var joinedCharacter = this.charactersService
                    .GetRegisteredCharacterForCurrentDungeon<CharacterNameRoleViewModel>(dungeon.Id, userId);

                if (joinedCharacter != null)
                {
                    dungeon.AlreadyJoined = true;
                    dungeon.JoinedCharacter = joinedCharacter;
                }
            }

            //var joinedCharacer = myCharacters.First(c => c.Dungeons.Any(d => d.DungeonId == dungeon.Id));
            //if (myCharacters.Any(c => c.Dungeons.Any(d => d.DungeonId == dungeon.Id)))

            //TODO: Code queility when finish !!!!!! IMPORTNAT !!!
            var raids = this.raidService
                .GetAll<RaidViewModel>()
                .AsEnumerable();

            var eventsIndexViewModel = new EventsIndexViewModel
            {
                IsUserRaidLeaderOrAdmin = this.User.IsInRole("Raid Leader") || this.User.IsInRole("Admin"),
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }
    }
}