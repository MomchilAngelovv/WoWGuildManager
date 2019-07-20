using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Logs;
using WowGuildManager.Models.ApiModels.Logs;
using WowGuildManager.Services.Api;
using WowGuildManager.Services.Gallery;

namespace WowGuildManager.Web.Areas.Api.Controllers
{
    public class LogsController : ApiController
    {
        private readonly IApiService apiService;

        public LogsController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        [Route("images")]
        public ActionResult<IEnumerable<ImageApiViewModel>> Images()
        {
            var images = this.apiService
                .GetAllImages<ImageApiViewModel>()
                .ToList();

            return images;
        }

        [Route("exceptions")]
        public ActionResult<IEnumerable<ExceptionApiViewModel>> Exceptions()
        {
            var exceptions = this.apiService
                .GetAllExceptions<ExceptionApiViewModel>()
                .ToList();

            return exceptions;
        }
    }
}
