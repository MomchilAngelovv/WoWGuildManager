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

//TODO: Api endpoits for dungeons /raids / characers everythng
namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class CharactersController : BaseController
    {
        private readonly ICharacterService characterService;
        private readonly UserManager<WowGuildManagerUser> userManager;

        public CharactersController(ICharacterService characterService, UserManager<WowGuildManagerUser> userManager)
        { 
            this.characterService = characterService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            
            var userId = await this.GetUserId(this.userManager);

            var characters = this.characterService
                .GetCharactersByUserId<CharacterViewModel>(userId)
                .ToList();

            var characterIndexViewModel = new CharacterIndexViewModel
            {
                Characters = characters
            };

            return View(characterIndexViewModel);
        }

        //TODO: Sort controllers dependanicyes and methods
        public IActionResult Create()
        {
            var classList = this.BindClassesToSelectListItem();
            var roleList = this.BindRolesToSelectListItem();

            this.ViewData["Classes"] = classList;
            this.ViewData["Roles"] = roleList;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CharacterCreateBindingModel model)
        {
            var userId = await this.GetUserId(this.userManager);

            model.UserId = userId;

            await this.characterService.CreateAsync(model);

            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string id)
        {
            var character = this.characterService
                .GetCharacterById<CharacterViewModel>(id);

            return this.View(character);
        }

        public IActionResult Delete(string id)
        {
            this.characterService.Delete(id);

            return this.RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> BindRolesToSelectListItem()
        {

            var roleList = this.characterService.GetRoles<SelectListItem>();

            return roleList;
        }

        private IEnumerable<SelectListItem> BindClassesToSelectListItem()
        {
            var classList = this.characterService.GetClasses<SelectListItem>();

            return classList;
        }
    }
}