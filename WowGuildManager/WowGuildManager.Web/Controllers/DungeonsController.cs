namespace WowGuildManager.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.BindingModels.Dungeons;

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

        [HttpGet]
        public IActionResult Create()
        {
            var myCharacters = this.BindCharactersToSelectListItem();

            if (myCharacters.Any() == false)
            {
                return this.RedirectToAction("Create", "Characters");
            }

            var dungeonPlaceList = this.BindDungeonPlacesToSelectListItem();

            this.ViewData["Characters"] = myCharacters;
            this.ViewData["Places"] = dungeonPlaceList;

            return this.View();
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var dungeonDetailsViewModel = this.dungeonService
                .GetDungeon<DungeonDetailsViewModel>(id);
            var registeredCharacters = this.dungeonService
                .GetRegisteredCharacters<CharacterDungeonDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);
            var myCharacters = this.characterService
                .GetUserCharacters<CharacterIdNameViewModel>(userId);

            dungeonDetailsViewModel.Characters = registeredCharacters;
            dungeonDetailsViewModel.AvailableCharacters = myCharacters;

            foreach (var myCharacter in myCharacters)
            {
                if (registeredCharacters.Any(regChar => regChar.Id == myCharacter.Id))
                {
                    var characterNameIdViewModel = this.characterService
                        .GetCharacter<CharacterIdNameViewModel>(myCharacter.Id);

                    dungeonDetailsViewModel.AlreadyJoined = true;
                    dungeonDetailsViewModel.JoinedCharacter = characterNameIdViewModel;
                    break;
                }
            }

            return this.View(dungeonDetailsViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> JoinAsync(string dungeonId, string characterId)
        {
            await this.dungeonService.RegisterCharacterAsync(dungeonId, characterId);
            return this.RedirectToAction("Upcoming", "Events");
        }
        [HttpGet]
        public async Task<IActionResult> KickAsync(string dungeonId, string characterId)
        {
            await this.dungeonService.KickCharacterAsync(dungeonId, characterId);
            return this.RedirectToAction("Upcoming", "Events");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DungeonCreateBindingModel inputModel)
        {
            await this.dungeonService.CreateAsync(inputModel);

            return RedirectToAction("Upcoming", "Events");
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(DungeonEditBindingModel input)
        {
            await this.dungeonService.EditAsync(input);
            return this.RedirectToAction(nameof(Details), new { id = input.DungeonId });
        }

        private IEnumerable<SelectListItem> BindCharactersToSelectListItem()
        {
            var userId = this.userManager.GetUserId(this.User);

            var myCharacters = this.characterService
                .GetUserCharacters<CharacterIdNameViewModel>(userId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                });

            return myCharacters;
        }
        private IEnumerable<SelectListItem> BindDungeonPlacesToSelectListItem()
        {
            var dungeonPlaceList = this.dungeonService.GetDestinations<SelectListItem>();
            return dungeonPlaceList;
        }
    }
}