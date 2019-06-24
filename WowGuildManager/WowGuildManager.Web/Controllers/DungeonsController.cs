using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Dungeons;

namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class DungeonsController : BaseController
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
            var myCharacters = await this.BindCharactersToSelectListItem();

            if (myCharacters.Any() == false)
            {
                return this.RedirectToAction("Create", "Characters");
            }

            var dungeonPlaceList = this.BindDungeonPlacesToSelectListItem();

            this.ViewData["Characters"] = myCharacters;
            this.ViewData["Places"] = dungeonPlaceList;

            return this.View();
        }

        private IEnumerable<SelectListItem> BindDungeonPlacesToSelectListItem()
        {
            var dungeonPlaceList = this.dungeonService.GetDestinations<SelectListItem>();

            return dungeonPlaceList;
        }

        private async Task<IEnumerable<SelectListItem>> BindCharactersToSelectListItem()
        {
            var userId = await this.GetUserId(this.userManager);

            var myCharacters = this.characterService
                .GetCharactersByUserId<CharacterIdNameViewModel>(userId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                });

            return myCharacters;
        }

        [HttpPost]
        public IActionResult Create(DungeonCreateBindingModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            this.dungeonService.Create(inputModel);

            return RedirectToAction("Upcoming", "Events");
        }

        //TODO: Think how to implement guild points
        public async Task<IActionResult> Details(string id)
        {
            var registeredCharacters = this.dungeonService
                .GetRegisteredCharactersByDungeonId<CharacterDungeonDetailsViewModel>(id);

            var userId = await this.GetUserId(this.userManager);

            var myCharacters = this.characterService
                .GetCharactersByUserId<CharacterIdNameViewModel>(userId);

            //TODO: Fix bug hwen it syas to register character when dungeon is full
            var dungeonDetailsViewModel = new DungeonDetailsViewModel
            {
                Id = id,
                Characters = registeredCharacters,
                AvailableCharacters = myCharacters
            };

            foreach (var myCharacter in myCharacters)
            {
                if (registeredCharacters.Any(rc => rc.Id == myCharacter.Id))
                {
                    var characterNameIdViewModel = this.characterService
                        .GetCharacterById<CharacterIdNameViewModel>(myCharacter.Id);

                    dungeonDetailsViewModel.AlreadyJoined = true;
                    dungeonDetailsViewModel.JoinedCharacter = characterNameIdViewModel;
                    break;
                }
            }

            return this.View(dungeonDetailsViewModel);
        }

        public IActionResult Join(string characterId, string dungeonId)
        {
            this.dungeonService.RegisterCharacter(characterId, dungeonId);

            return this.RedirectToAction("Upcoming", "Events");
        }
    }
}