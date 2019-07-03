namespace WowGuildManager.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using WowGuildManager.Domain.Identity;

    public abstract class BaseController : Controller
    {
        protected async Task<string> GetUserId(UserManager<WowGuildManagerUser> userManager)
        {
            
            var userId = (await userManager.GetUserAsync(this.User)).Id;
            
            return userId;
        }
    }
}
