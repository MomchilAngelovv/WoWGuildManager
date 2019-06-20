﻿using System;
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
    public class EventsController : Controller
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly WowGuildManagerDbContext context;
        private readonly IDungeonService dungeonService;
        private readonly IRaidService raidService;
        private readonly ICharacterService charactersService;

        //TODO: All view Model will be IEnumerables
        public EventsController(
            UserManager<WowGuildManagerUser> userManager,
            WowGuildManagerDbContext context,
            IDungeonService dungeonService,
            IRaidService raidService,
            ICharacterService charactersService)
        {
            this.userManager = userManager;
            this.context = context;
            this.dungeonService = dungeonService;
            this.raidService = raidService;
            this.charactersService = charactersService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = (await this.userManager.GetUserAsync(this.User)).Id;

            //TODO: REMOVE DB ENTITY
            var myCharacters = this.charactersService
                .GetCharactersByUserId<Character>(userId)
                .AsEnumerable();

            //TODO: Remove all  mapping from controllers
            var dungeons = this.dungeonService
                .GetAll<DungeonViewModel>()
                .ToList();

            //TODO: 10000000% REFACTOR THIS WITH MAPPER
            for (int i = 0; i < dungeons.Count; i++)
            {
                if (myCharacters.Any(c => c.Dungeons.Any(d => d.DungeonId == dungeons[i].Id)))
                {
                    var joinedCharacer = myCharacters.First(c => c.Dungeons.Any(d => d.DungeonId == dungeons[i].Id));

                    dungeons[i].AlreadyJoined = true;
                    dungeons[i].JoinedCharacterName = joinedCharacer.Name;
                    dungeons[i].JoinedCharacterLevel = joinedCharacer.Level.ToString();
                    dungeons[i].JoinedCharacterRole = joinedCharacer.Role.ToString();
                }
            }

            var raids = this.raidService
                .GetAll<RaidViewModel>()
                .AsEnumerable();

            var eventsIndexViewModel = new EventsIndexViewModel
            {
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }
    }
}