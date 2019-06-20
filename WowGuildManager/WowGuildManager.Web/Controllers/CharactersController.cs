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
using WowGuildManager.Services.Characters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WowGuildManager.Web.Mapper;
using System.Security.Claims;

namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class CharactersController : Controller
    {
        private readonly UserManager<WowGuildManagerUser> userManager;

        private readonly ICharacterService characterService;

        public CharactersController(
            UserManager<WowGuildManagerUser> userManager,
            ICharacterService characterService)
        {
            this.userManager = userManager;
            this.characterService = characterService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = (await this.userManager.GetUserAsync(this.User)).Id;

            var characters = this.characterService
                .GetCharactersByUserId<CharacterViewModel>(userId)
                .AsEnumerable();

            var characterIndexViewModel = new CharacterIndexViewModel
            {
                Characters = characters
            };

            return View(characterIndexViewModel);
        }

        public IActionResult Create()
        {
            var classList = this.BindClassesToSelectListItem();
            var roleList = this.BindRolesToSelectListItem();

            this.ViewData["Classes"] = classList;
            this.ViewData["Roles"] = roleList;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CharacterCreateInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return this.RedirectToAction(nameof(Create));
            }

            var userId = (await this.userManager.GetUserAsync(this.User)).Id;

            inputModel.UserId = userId;

            this.characterService.Create(inputModel);

            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string id)
        {
            var character = this.characterService.GetCharacterById<CharacterViewModel>(id);

            return this.View(character);
        }

        public IActionResult Delete(string id)
        {
            this.characterService.Delete(id);

            return this.RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> BindRolesToSelectListItem()
        {

            var roleList = this.characterService.GetRoles()
               .Select(cl => new SelectListItem
               {
                   Text = cl.ToString(),
                   Value = ((int)cl).ToString()
               });

            return roleList;
        }

        private IEnumerable<SelectListItem> BindClassesToSelectListItem()
        {
            var classList = this.characterService.GetClasses()
              .Select(cl => new SelectListItem
              {
                  Text = cl.ToString(),
                  Value = ((int)cl).ToString()
              });

            return classList;
        }
    }
}