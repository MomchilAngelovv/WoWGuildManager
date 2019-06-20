using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Services.Characters;

namespace WowGuildManager.Web.Controllers
{
    [AllowAnonymous]
    public class MembersController : Controller
    {
        private readonly ICharacterService characterService;

        public MembersController(
            ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        //TODO: Add Ranks to models
        public IActionResult Index()
        {
            var members = this.characterService
                .GetAll<CharacterViewModel>()
                .AsEnumerable();

            var membersIndexViewModel = new MembersIndexViewModel
            {
                Members = members
            };

            return this.View(membersIndexViewModel);
        }
    }
}