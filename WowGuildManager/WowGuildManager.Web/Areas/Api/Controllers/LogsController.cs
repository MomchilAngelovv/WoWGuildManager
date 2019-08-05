namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Services.Api;
    using WowGuildManager.Models.ApiModels.Logs;

    public class LogsController : ApiController
    {
        private readonly IApiService apiService;

        public LogsController(
            IApiService apiService)
        {
            this.apiService = apiService;
        }

        [Route("images")]
        public ActionResult<IEnumerable<ImageApiViewModel>> Images()
        {
            var images = this.apiService
                .GetAllImages()
                .ToList();

            return images;
        }

        [Route("exceptions")]
        public ActionResult<IEnumerable<ExceptionApiViewModel>> Exceptions()
        {
            var exceptions = this.apiService
                .GetAllExceptions()
                .ToList();

            return exceptions;
        }
    }
}
