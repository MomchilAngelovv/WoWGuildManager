namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Services.Api;
   
    public class MembersController : ApiController
    {
        private readonly IApiService apiService;

        public MembersController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        [HttpGet]
        public IEnumerable<CharacterApiViewModel> Get()
        {
            var members = this.apiService.Members<CharacterApiViewModel>();
            return members;
        }

        [HttpGet("{id}")]
        public CharacterApiViewModel Get(string id)
        {
            var character = this.apiService.GetCharacterById<CharacterApiViewModel>(id);

            return character;
        }
    }
}
