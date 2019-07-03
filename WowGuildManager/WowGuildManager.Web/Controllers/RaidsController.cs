namespace WowGuildManager.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.BindingModels.Raids;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Services.Raids;

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
                .GetCharactersByUserId<SelectListItem>(userId);
               
            return myCharacters;
        }

        private IEnumerable<SelectListItem> BindRaidPlacesToSelectListItem()
        {
            var raidPlaceList = this.raidService
                .GetDestinations<SelectListItem>();

            return raidPlaceList;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RaidCreateBindingModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            await this.raidService.CreateAsync(inputModel);

            return RedirectToAction("Upcoming", "Events");
        }   

        public async Task<IActionResult> Details(string id)
        {
            var userId = await this.GetUserId(this.userManager);

            var registeredCharacters = this.raidService
                 .GetRegisteredCharactersByRaidId<CharacterRaidDetailsViewModel>(id)
                 .ToList();

            var myCharacters = this.characterService
                .GetCharactersByUserId<CharacterIdNameViewModel>(userId)
                .ToList();

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

        public async Task<IActionResult> Join(string characterId, string raidId)
        {
            await this.raidService.RegisterCharacterAsync(characterId, raidId);

            return this.RedirectToAction("Upcoming", "Events");
        }
    }
}