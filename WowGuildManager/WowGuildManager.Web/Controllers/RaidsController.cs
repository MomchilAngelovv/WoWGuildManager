using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.Controllers
{
    public class RaidsController : BaseController
    {
        private readonly IRaidService raidService;
        private readonly ICharacterService characterService;
        private readonly UserManager<WowGuildManagerUser> userManager;

        public RaidsController(
            IRaidService raidService, 
            ICharacterService characterService,
            UserManager<WowGuildManagerUser> userManager)
        {
            this.raidService = raidService;
            this.characterService = characterService;
            this.userManager = userManager;
        }

        //TODO: Security and authority validation everyWHERE !! IMPORTANTT !!!!
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Create()
        {
            var myCharacters = await this.BindCharactersToSelectListItem();

            if (myCharacters.Any() == false)
            {
                return this.RedirectToAction("Create", "Characters");
            }

            var placeList = this.BindRaidPlacesToSelectListItem();

            this.ViewData["Characters"] = myCharacters;
            this.ViewData["Places"] = placeList;

            return this.View();
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

        private IEnumerable<SelectListItem> BindRaidPlacesToSelectListItem()
        {
            var raidPlaceList = this.raidService.GetDestinations<SelectListItem>();

            return raidPlaceList;
        }

        [HttpPost]
        public IActionResult Create(RaidCreateBindingModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            this.raidService.Create(inputModel);

            return RedirectToAction("Index", "Events");
        }

        public async Task<IActionResult> Details(string id)
        {
            var userId = await this.GetUserId(this.userManager);

            var registeredCharacters = this.raidService
                 .GetRegisteredCharactersByRaidId<CharacterRaidDetailsViewModel>(id)
                 .AsEnumerable();

            var myCharacters = this.characterService
                .GetCharactersByUserId<CharacterIdNameViewModel>(userId)
                .AsEnumerable();

            var raidDetailsViewModel = new RaidDetailsViewModel
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

                    raidDetailsViewModel.AlreadyJoined = true;
                    raidDetailsViewModel.JoinedCharacter = characterNameIdViewModel;
                    break;
                }
            }

            return this.View(raidDetailsViewModel);
        }

        public IActionResult Join(string characterId, string raidId)
        {
            this.raidService.RegisterCharacter(characterId, raidId);

            return this.RedirectToAction("Index", "Events");
        }
    }
}