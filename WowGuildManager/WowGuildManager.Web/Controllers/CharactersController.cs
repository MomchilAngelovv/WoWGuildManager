//TODO: COnsier create input forms to make models in Get methods and return view models
//TODO: Api endpoits for dungeons /raids / characers everythng
//TODO: Consider remove selectitem list
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
            this.characterService = characterService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var characters = this.characterService
                .GetCharactersByUserId<CharacterViewModel>(userId);

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
            var userId = this.userManager.GetUserId(this.User);

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

        public IActionResult Edit(string id)
        {
            var characterEditBindingModel = this.characterService
                .GetCharacterById<CharacterEditBindingModel>(id);

            var roleList = this.BindRolesToSelectListItem();

            this.ViewData["Roles"] = roleList;

            return this.View(characterEditBindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CharacterEditBindingModel model)
        {
            await this.characterService.Edit(model);
            return this.RedirectToAction(nameof(Index));
        }
    }
}