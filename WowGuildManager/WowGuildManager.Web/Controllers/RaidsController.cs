namespace WowGuildManager.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Services.Raids;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.BindingModels.Raids;
    using WowGuildManager.Models.ViewModels.Characters;

    [Authorize]
    public class RaidsController : BaseController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IRaidService raidService;
        private readonly ICharacterService characterService;

        public RaidsController(
            UserManager<WowGuildManagerUser> userManager,
            IRaidService raidService, 
            ICharacterService characterService)
        {
            this.userManager = userManager;
            this.raidService = raidService;
            this.characterService = characterService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var myCharacters = this.BindCharactersToSelectListItem();

            if (myCharacters.Any() == false)
            {
                return this.RedirectToAction("Create", "Characters");
            }

            var placeList = this.BindRaidPlacesToSelectListItem();

            this.ViewData["Characters"] = myCharacters;
            this.ViewData["Places"] = placeList;

            return this.View();
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var raidDetailsViewModel = this.raidService.GetRaid<RaidDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);

            var registeredCharacters = this.raidService
                 .GetRegisteredCharacters<CharacterRaidDetailsViewModel>(id);

            var myCharacters = this.characterService
                .GetUserCharacters<CharacterIdNameViewModel>(userId);

            raidDetailsViewModel.Characters = registeredCharacters;
            raidDetailsViewModel.AvailableCharacters = myCharacters;

            foreach (var myCharacter in myCharacters)
            {
                if (registeredCharacters.Any(rc => rc.Id == myCharacter.Id))
                {
                    var characterNameIdViewModel = this.characterService
                        .GetCharacter<CharacterIdNameViewModel>(myCharacter.Id);

                    raidDetailsViewModel.AlreadyJoined = true;
                    raidDetailsViewModel.JoinedCharacter = characterNameIdViewModel;
                    break;
                }
            }

            return this.View(raidDetailsViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> JoinAsync(string characterId, string raidId)
        {
            await this.raidService.RegisterCharacterAsync(raidId, characterId);

            return this.RedirectToAction("Upcoming", "Events");
        }
        [HttpGet]
        public async Task<IActionResult> KickAsync(string characterId, string raidId)
        {
            await this.raidService.KickPlayerAsync(characterId, raidId);
            return this.RedirectToAction(nameof(Details), new { id = raidId});
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RaidCreateBindingModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            await this.raidService.CreateAsync(inputModel);

            return RedirectToAction("Upcoming", "Events");
        }   
        [HttpPost]
        public async Task<IActionResult> EditAsync(RaidEditBindingModel input)
        {
            await this.raidService.EditAsync(input);

            return this.RedirectToAction(nameof(Details),new { id = input.RaidId });
        }

        private IEnumerable<SelectListItem> BindCharactersToSelectListItem()
        {
            var userId = this.userManager.GetUserId(this.User);

            var myCharacters = this.characterService
                .GetUserCharacters<SelectListItem>(userId);

            return myCharacters;
        }
        private IEnumerable<SelectListItem> BindRaidPlacesToSelectListItem()
        {
            var raidPlaceList = this.raidService
                .GetDestinations<SelectListItem>();

            return raidPlaceList;
        }
    }
}