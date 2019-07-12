namespace WowGuildManager.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.BindingModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Services.Dungeons;

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

        private IEnumerable<SelectListItem> BindDungeonPlacesToSelectListItem()
        {
            var dungeonPlaceList = this.dungeonService.GetDestinations<SelectListItem>();

            return dungeonPlaceList;
        }

        private IEnumerable<SelectListItem> BindCharactersToSelectListItem()
        {
            var userId = this.userManager.GetUserId(this.User);

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
        public async Task<IActionResult> Create(DungeonCreateBindingModel inputModel)
        {
            await this.dungeonService.CreateAsync(inputModel);

            return RedirectToAction("Upcoming", "Events");
        }

        //TODO: Think how to implement guild points
        public IActionResult Details(string id)
        {
            var dungeonDetailsViewModel = this.dungeonService.GetDungeon<DungeonDetailsViewModel>(id);

            var registeredCharacters = this.dungeonService
                .GetRegisteredCharactersByDungeonId<CharacterDungeonDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);

            var myCharacters = this.characterService
                .GetCharactersByUserId<CharacterIdNameViewModel>(userId);

            //TODO: Fix bug hwen it syas to register character when dungeon is full
            dungeonDetailsViewModel.Characters = registeredCharacters;
            dungeonDetailsViewModel.AvailableCharacters = myCharacters;

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

        public async Task<IActionResult> Join(string characterId, string dungeonId)
        {
            await this.dungeonService.RegisterCharacterAsync(characterId, dungeonId);

            return this.RedirectToAction("Upcoming", "Events");
        }

        public async Task<IActionResult> Kick(string characterId, string dungeonId)
        {
            await this.dungeonService.KickCharacter(characterId, dungeonId);
            return this.RedirectToAction("Upcoming", "Events");
        }
    }
}