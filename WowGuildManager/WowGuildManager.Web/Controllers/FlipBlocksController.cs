namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class FlipBlocksController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
