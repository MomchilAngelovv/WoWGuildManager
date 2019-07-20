//TODO: KUZMATA: WORLD BOSOVE TRQBVA DA DOBAVISH !!!!
namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Api;
    using WowGuildManager.Models.ApiModels.Raids;
    using System.Linq;

    public class GuildController : ApiController
    {
        private readonly IApiService apiService;

        public GuildController(
            IApiService apiService)
        {
            this.apiService = apiService;
        }

        [Route("Progress")]
        public ActionResult<IEnumerable<RaidDestinationProgressApiViewModel>> Progress()
        {
            var raidDestinations = this.apiService
                .GuildProgress<RaidDestinationProgressApiViewModel>()
                .ToList();
            
            return raidDestinations;
        }
    }
}
