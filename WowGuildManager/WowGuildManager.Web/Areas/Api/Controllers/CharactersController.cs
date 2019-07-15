namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Services.Api;

    [Authorize]
    public class CharactersController : ApiController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;

        private readonly IApiService apiService;
      
        public CharactersController(
            UserManager<WowGuildManagerUser> userManager,
            IApiService apiService)
        {
            this.userManager = userManager;
            this.apiService = apiService;
        }
    
        [HttpGet]
        public IEnumerable<CharacterApiViewModel> Get()
        {
            var userId = this.userManager.GetUserId(this.User);

            var characters = this.apiService.GetAll<CharacterApiViewModel>(userId);

            return characters;
        }
    }
}