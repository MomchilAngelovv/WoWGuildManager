using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ApiModels.Characters;
using WowGuildManager.Services.Api;

namespace WowGuildManager.Web.Areas.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Area("Api")]
    [ApiController]
    public class CharactersController : ControllerBase
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
    
        //TODO: Separate Input model from view models
        public IEnumerable<CharacterApiViewModel> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            return this.apiService.GetAll<CharacterApiViewModel>(userId).ToList();
        }

        [Route("{id}")]
        public CharacterApiViewModel GetCharacterById(string id)
        {
            return this.apiService.GetCharacterById<CharacterApiViewModel>(id);
        }
    }
}