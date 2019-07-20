
//TODO: Bootstrap tooltips
namespace WowGuildManager.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Characters;

    [AllowAnonymous]
    public class MembersController : BaseController
    {
        private readonly ICharacterService characterService;

        public MembersController(
            ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        public IActionResult All()
        {
            var members = this.characterService
                .GetAll<CharacterViewModel>()
                .ToList();

            var membersIndexViewModel = new MembersIndexViewModel
            {
                Members = members
            };

            return this.View(membersIndexViewModel);
        }
    }
}