namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [AllowAnonymous]
    public class FlipBlocksController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
