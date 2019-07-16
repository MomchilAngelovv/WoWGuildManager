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

    [AllowAnonymous]
    public class GalleryController : BaseController
    {
        private readonly UserManager<WowGuildManagerUser> userManager;

        private readonly Cloudinary cloudinary;
        private readonly WowGuildManagerDbContext context;

        public GalleryController(
            UserManager<WowGuildManagerUser> userManager,
            Cloudinary cloudinary,
            WowGuildManagerDbContext context)
        {
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "Gallery",
                        File = new FileDescription(Guid.NewGuid().ToString(), stream)
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    await this.InsertUploadLogAsync(uploadResult);
                }
            }

            return this.RedirectToAction(nameof(Index));
        }

        private async Task<ImageUploadLog> InsertUploadLogAsync(ImageUploadResult uploadResult)
        {
            var userId = this.userManager.GetUserId(this.User);

            var imageUploadLog = new ImageUploadLog
            {
                CreatedAt = uploadResult.CreatedAt,
                Format = uploadResult.Format,
                Length = uploadResult.Length,
                UserId = userId,
                Url = uploadResult.SecureUri.AbsoluteUri
            };

            await this.context.ImageUploadLogs.AddAsync(imageUploadLog);
            await this.context.SaveChangesAsync();

            return null;
        }
    }
}