namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using WowGuildManager.Services.Api;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.ApiModels.Characters;

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