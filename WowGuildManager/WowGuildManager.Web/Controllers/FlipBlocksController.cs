namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FlipBlocksController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
