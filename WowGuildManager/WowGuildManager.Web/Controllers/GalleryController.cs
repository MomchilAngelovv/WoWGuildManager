//TODO: Require unique email;
namespace WowGuildManager.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Gallery;
    using WowGuildManager.Models.ViewModels.Gallery;
    using WowGuildManager.Common.GlobalConstants;

    [AllowAnonymous]
    public class GalleryController : BaseController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IGalleryService galleryService;

        public GalleryController(
            UserManager<WowGuildManagerUser> userManager,
            IGalleryService galleryService)
        {
            this.userManager = userManager;
            this.galleryService = galleryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var images = this.galleryService
                .GetAllImages<GalleryImageViewModel>();

            var galleryIndexViewModel = new GalleryIndexViewModel
            {
                Images = images
            };

            return View(galleryIndexViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GuildRanksConstants.GuildMaster)]
        public async Task<IActionResult> MakeNonActualAsync(string id)
        {
            await this.galleryService.RemoveImageAsync(id);
            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = GuildRanksConstants.GuildMaster)]
        public async Task<IActionResult> UploadImageAsync(List<IFormFile> files)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.galleryService.UploadImagesAsync(files, userId);
            return this.RedirectToAction(nameof(Index));
        }
    }
}