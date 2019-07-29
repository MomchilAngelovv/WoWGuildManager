namespace WowGuildManager.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.BindingModels.Characters;

    [Authorize]
    public class CharactersController : BaseController
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

        [HttpGet]
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var characters = this.characterService
                .GetUserCharacters<CharacterViewModel>(userId);

            var characterIndexViewModel = new CharacterIndexViewModel
            {
                Characters = characters
            };

            return View(characterIndexViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var classList = this.BindClassesToSelectListItem();
            var roleList = this.BindRolesToSelectListItem();

            this.ViewData["Classes"] = classList;
            this.ViewData["Roles"] = roleList;

            return this.View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var characterEditBindingModel = this.characterService
                .GetCharacter<CharacterEditBindingModel>(id);

            var roleList = this.BindRolesToSelectListItem();
            this.ViewData["Roles"] = roleList;

            return this.View(characterEditBindingModel);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var character = this.characterService
                .GetCharacter<CharacterViewModel>(id);

            return this.View(character);
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var characterDeleteViewModel = this.characterService
                .GetCharacter<CharacterDeleteViewModel>(id);

            return this.View(characterDeleteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CharacterCreateBindingModel createModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            createModel.UserId = userId;
            await this.characterService.CreateAsync(createModel);

            this.TempData["NewCharacter"] = "Thank you for registering new character!";
            return this.RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(CharacterEditBindingModel editModel)
        {
            await this.characterService.EditAsync(editModel);
            return this.RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this.characterService
                .DeleteAsync(id);

            return this.RedirectToAction(nameof(Index));
        }   
       
        private IEnumerable<SelectListItem> BindClassesToSelectListItem()
        {
            var classList = this.characterService.GetClassList<SelectListItem>();
            return classList;
        }   
        private IEnumerable<SelectListItem> BindRolesToSelectListItem()
        {
            var roleList = this.characterService.GetRoleList<SelectListItem>();
            return roleList;
        }
    }
}