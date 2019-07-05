using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Models.ApiModels.Raids;
using WowGuildManager.Models.ViewModels.Guild;
using WowGuildManager.Services.Api;
using WowGuildManager.Services.Raids;
using WowGuildManager.Web.Controllers;

//TODO: KUZMATA: WORLD BOSOVE TRQBVA DA DOBAVISH !!!!
namespace WowGuildManager.Web.Areas.Api.Controllers
{
    public class GuildController : ApiController
    {
        private readonly IApiService apiService;

        public GuildController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        [Route("Progress")]
        public IEnumerable<RaidDestinationProgressApiViewModel> Progress()
        {
            var raidDestinations = this.apiService.GuildProgress<RaidDestinationProgressApiViewModel>();
            
            return raidDestinations;
        }
    }
}
