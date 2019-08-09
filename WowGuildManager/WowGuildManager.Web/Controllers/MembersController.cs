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

        [HttpGet]
        public IActionResult All(string sortOrder, string searchName = null)
        {
            var members = this.characterService
                .GetAllCharacters<CharacterViewModel>()
                .ToList();

            var totalMembersCount = members.Count;

            switch (sortOrder)
            {
                case "level":
                    members = members.OrderByDescending(m => m.Level).ToList();
                    break;
                case "class":
                    members = members.OrderByDescending(m => m.Class).ToList();
                    break;
                case "role":
                    members = members.OrderByDescending(m => m.Role).ToList();
                    break;
                case "name":
                    members = members.OrderByDescending(m => m.Name).ToList();
                    break;
            }

            if (searchName != null)
            {
                members = members.Where(m => m.Name.Contains(searchName)).ToList();
            }

            var membersIndexViewModel = new MembersIndexViewModel
            {
                Members = members,
                MembersCount = totalMembersCount
            };

            return this.View(membersIndexViewModel);
        }
    }
}