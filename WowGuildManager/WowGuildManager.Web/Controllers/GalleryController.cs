using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WowGuildManager.Web.Controllers
{
    [AllowAnonymous]
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}