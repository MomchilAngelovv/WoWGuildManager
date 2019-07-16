using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using WowGuildManager.Data;

namespace WowGuildManager.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly WowGuildManagerDbContext context;

        public GalleryService(WowGuildManagerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> GetAllGallery()
        {
            var images = this.context.ImageUploadLogs
                .Select(ui => ui.Url)
                .ToList();

            return images;
        }
    }
}
