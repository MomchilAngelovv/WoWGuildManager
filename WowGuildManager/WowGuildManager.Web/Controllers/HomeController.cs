﻿//TODO: MAKE EVEYEWHERE BUTTON-SM !!
//TODO APPLY Authorization attribites everywhere
namespace WowGuildManager.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.ViewModels.Error;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //TODO: Intoduce Privacy policy page
            return View();
        }

        //TODO: COnsider leaveing sections and some few divs outside partial views
        //TODO: Consired proper error view (search in net)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
