﻿namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Api;
    using WowGuildManager.Models.ApiModels.Characters;
   
    public class MembersController : ApiController
    {
        private readonly IApiService apiService;

        public MembersController(
            IApiService apiService)
        {
            this.apiService = apiService;
        }

        [HttpGet]
        public IEnumerable<CharacterApiViewModel> Get()
        {
            var members = this.apiService.GetAllMembers();
            return members;
        }

        [HttpGet("{id}")]
        public CharacterApiViewModel Get(string id)
        {
            var character = this.apiService.GetCharacter(id);
            return character;
        }
    }
}
