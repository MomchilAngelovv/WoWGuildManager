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
        private readonly IMapper mapper;

        public MembersController(
            ICharacterService characterService,
            IMapper mapper)
        {
            this.characterService = characterService;
            this.mapper = mapper;
        }

        //TODO: Add Ranks to models
        public IActionResult Index()
        {
            var allMembers = this.characterService.GetAll()
                .Select(character => mapper.Map<CharacterViewModel>(character));

            var membersIndexViewModel = new MembersIndexViewModel
            {
                Members = allMembers
            };

            return View(membersIndexViewModel);
        }
    }
}