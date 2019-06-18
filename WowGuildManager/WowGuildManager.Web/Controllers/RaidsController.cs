using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.Controllers
{
    [Authorize(Roles = "Admin, Raid Leader")]
    public class RaidsController : Controller
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

        public IActionResult Index()
        {
            return View();
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

            var places = this.raidService.GetPlaces()
               .Select(place => new SelectListItem
               {
                   Text = place.ToString(),
                   Value = ((int)place).ToString()
               });

            this.ViewBag.Characters = myCharacters;
            this.ViewBag.Places = places;

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(RaidCreateInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Create));
            }

            this.raidService.Create(inputModel);

            return RedirectToAction("Index", "Events");
        }
    }
}