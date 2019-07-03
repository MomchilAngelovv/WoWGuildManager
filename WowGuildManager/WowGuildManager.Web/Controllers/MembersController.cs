﻿
//TODO: Bootstrap tooltips
//TODO: CHECK CSS EVEYRHWEW AND FIX CLASSES
namespace WowGuildManager.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Services.Characters;

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