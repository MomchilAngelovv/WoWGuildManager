//TODO: KUZMATA: WORLD BOSOVE TRQBVA DA DOBAVISH !!!!
namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Models.ApiModels.Raids;
    using WowGuildManager.Services.Api;

    public class GuildController : ApiController
    {
        private readonly IApiService apiService;

        public GuildController(
            IApiService apiService)
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
