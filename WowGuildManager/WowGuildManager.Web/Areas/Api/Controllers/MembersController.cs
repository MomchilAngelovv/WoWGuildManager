namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Services.Api;

    [Area("Api")]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IApiService apiService;

        public MembersController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public IEnumerable<CharacterApiViewModel> AllMembers()
        {
            var members = this.apiService.Members<CharacterApiViewModel>();
            return members;
        }

        [Route("{id}")]
        public CharacterApiViewModel GetMemberById(string id)
        {
            var character = this.apiService.GetCharacterById<CharacterApiViewModel>(id);

            return character;
        }
    }
}
