namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Api;
    using WowGuildManager.Models.ApiModels.Raids;

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
                .GuildProgress()
                .ToList();
            
            return raidDestinations;
        }
    }
}
