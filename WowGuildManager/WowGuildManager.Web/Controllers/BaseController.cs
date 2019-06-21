using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Web.Controllers
{
    //TODO: Consider moving raid and dungeon listing in partial views
    public class BaseController : Controller
    {
        protected async Task<string> GetUserId(UserManager<WowGuildManagerUser> userManager)
        {
            var userId = (await userManager.GetUserAsync(this.User)).Id;

            return userId;
        }
    }
}
