//TODO: Require unique email;

namespace WowGuildManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using CloudinaryDotNet.Actions;
    using CloudinaryDotNet;
    using System.Threading.Tasks;
    using System;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Data;
    using WowGuildManager.Domain.Identity;
    using Microsoft.AspNetCore.Identity;
    using WowGuildManager.Models.ViewModels.Gallery;
    using WowGuildManager.Services.Gallery;

    [AllowAnonymous]
    public class GalleryController : BaseController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;

        private readonly Cloudinary cloudinary;
        private readonly WowGuildManagerDbContext context;
        private readonly IGalleryService galleryService;

        public GalleryController(
            UserManager<WowGuildManagerUser> userManager,
            Cloudinary cloudinary,
            WowGuildManagerDbContext context,
            IGalleryService galleryService)
        {
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.context = context;
            this.galleryService = galleryService;
        }

        public async Task<IActionResult> MakeNonActualAsync(string id)
        {
            await this.galleryService.RemoveImageAsync(id);
            
            return this.RedirectToAction(nameof(Index));
        }

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

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(List<IFormFile> files)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.galleryService.UploadImagesAsync(files, userId);

            return this.RedirectToAction(nameof(Index));
        }
    }
}