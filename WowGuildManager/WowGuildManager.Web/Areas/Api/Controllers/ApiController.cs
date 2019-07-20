namespace WowGuildManager.Web.Areas.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WowGuildManager.Services.Api;

    [Route("api/[controller]")]
    [Area("Api")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
       
    }
}
