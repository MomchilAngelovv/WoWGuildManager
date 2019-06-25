using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowGuildManager.Web.Controllers
{
    public class GuildController : BaseController
    {
        public IActionResult Progress()
        {
            return this.View();
        }
    }
}
