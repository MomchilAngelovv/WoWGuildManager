//TODO: Make carousel links only on GO TO ....
namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    //TODO: Require unique email;
    [AllowAnonymous]
    public class GalleryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}