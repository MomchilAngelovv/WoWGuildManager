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
    public class DungeonsController : Controller
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IDungeonService dungeonService;
        private readonly ICharacterService characterService;
        private readonly IMapper mapper;

        public DungeonsController(
            UserManager<WowGuildManagerUser> userManager,
            IDungeonService dungeonService,
            ICharacterService characterService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.dungeonService = dungeonService;
            this.characterService = characterService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var myCharacters = this.characterService.GetCharactersByUser(user)
                .Select(character => new SelectListItem
                {
                    Text = character.Name,
                    Value = character.Id
                });

            if (myCharacters.Any() == false)
            {
                return this.RedirectToAction("Create", "Characters");
            }

            var dungeonPlaces = this.dungeonService.GetPlaces()
              .Select(place => new SelectListItem
              {
                  Text = place.ToString(),
                  Value = ((int)place).ToString()
              });

            this.ViewBag.Characters = myCharacters;
            this.ViewBag.Places = dungeonPlaces;

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(DungeonCreateInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            this.dungeonService.Create(inputModel);

            return RedirectToAction("Index", "Events");
        }

        public async Task<IActionResult> Details(string id)
        {
            var registeredCharacters = this.characterService
                .GetCharactersForDungeonByDungeonId(id)
                .Select(c => mapper.Map<CharacterViewModel>(c))
                .ToList();

            var user = await this.userManager.GetUserAsync(this.User);

            var myCharacters = this.characterService.GetCharactersByUser(user)
               .Select(character => new CharacterJoinViewModel
               {
                   Id = character.Id,
                   Name = character.Name
               })
               .ToList();

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
                    dungeonDetailsViewModel.AlreadyJoined = true;
                    dungeonDetailsViewModel.JoinedCharacterId = myCharacter.Id;
                    dungeonDetailsViewModel.JoinedCharacterName = myCharacter.Name;
                    break;
                }
            }

            return this.View(dungeonDetailsViewModel);
        }

        public IActionResult Join(string characterId, string dungeonId)
        {
            this.dungeonService.RegisterCharacter(characterId, dungeonId);

            return this.RedirectToAction("Index", "Events");
        }
    }
}