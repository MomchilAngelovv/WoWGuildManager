using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//TODO: Make carousel links only on GO TO ....
namespace WowGuildManager.Web.Controllers
{
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