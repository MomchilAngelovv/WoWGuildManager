﻿using System;
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
            var user = await this.userManager.GetUserAsync(this.User);

            //TODO: COnfigure AutoMapper
            var characters = this.characterService.GetCharactersByUser(user)
                .Select(character => new CharacterViewModel
                {
                    Id = character.Id,
                    Class = character.Class.ToString(),
                    Level = character.Level.ToString(),
                    Name = character.Name,
                    Role = character.Role.ToString(),
                    Image = character.Image
                })
                .ToList();
               
               
            var characterIndexViewModel = new CharacterIndexViewModel
            {
                Characters = characters
            };

            return View(characterIndexViewModel);
        }

        
        public IActionResult Create()
        {
            var classes = this.characterService.GetClasses()
                .Select(cl => new SelectListItem
                {
                    Text = cl.ToString(),
                    Value = ((int)cl).ToString()
                });

            var roles = this.characterService.GetRoles()
               .Select(cl => new SelectListItem
               {
                   Text = cl.ToString(),
                   Value = ((int)cl).ToString()
               });

            this.ViewBag.Classes = classes;
            this.ViewBag.Roles = roles;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CharacterCreateViewModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return this.RedirectToAction(nameof(Create));
            }

            var user = await this.userManager.GetUserAsync(this.User);

            this.characterService.Create(inputModel, user);

            return this.RedirectToAction(nameof(Index));
        }

        private IActionResult Details(string id)
        {
            return this.View();
        }
    }
}