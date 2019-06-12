﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Dungeons;

namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class DungeonsController : Controller
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IDungeonService dungeonService;
        private readonly ICharacterService characterService;

        public DungeonsController(
            UserManager<WowGuildManagerUser> userManager,
            IDungeonService dungeonService,
            ICharacterService characterService)
        {
            this.userManager = userManager;
            this.dungeonService = dungeonService;
            this.characterService = characterService;
        }

        public async Task<IActionResult> Create()
        {
            this.ViewBag.Places = this.dungeonService.GetPlaces()
                .Select(place => new SelectListItem
                {
                    Text = place.ToString(),
                    Value = ((int)place).ToString()
                });

            var user = await this.userManager.GetUserAsync(this.User);

            this.ViewBag.Characters = this.characterService.GetCharactersByUser(user)
                .Select(character => new SelectListItem
                {
                    Text = character.Name,
                    Value = character.Id
                });

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(DungeonCreateViewModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            this.dungeonService.Create(inputModel);

            return RedirectToAction("Index", "Events");
        }

        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}