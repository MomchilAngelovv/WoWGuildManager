using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Services.Api;

namespace WowGuildManager.Web.Areas.Api.Controllers
{
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

        [Route("api/[controller]/all")]
        public IEnumerable<Character> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            return this.apiService.GetAll(userId).ToList();
        }

        [Route("api/[controller]/{id}")]
        public Character GetCharacterById(string id)
        {
            return this.apiService.GetCharacterById(id);
        }
    }
}