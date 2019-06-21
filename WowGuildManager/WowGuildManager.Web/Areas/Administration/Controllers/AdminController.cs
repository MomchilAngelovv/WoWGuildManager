using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WowGuildManager.Web.Areas.Administration.Controllers
{
    //TODO: MAKE DB SEEDERS
    //TODO: Put bootstrap icons
    [Area("Administration")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}