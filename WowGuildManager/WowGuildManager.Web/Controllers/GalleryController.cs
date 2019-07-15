//TODO: Require unique email;

namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [AllowAnonymous]
    public class GalleryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}