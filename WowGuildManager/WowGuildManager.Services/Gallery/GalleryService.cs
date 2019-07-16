using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;

namespace WowGuildManager.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public GalleryService(
            WowGuildManagerDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAllGallery<T>()
        {
            var images = this.context.ImageUploadLogs
                .Where(i => i.IsActual)
                .Select(i=> mapper.Map<T>(i));

            return images;
        }

        public async Task MakeNonActualAsync(string imageId)
        {
            var image = this.context.ImageUploadLogs
                .Find(imageId);

            if (image == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidImageErrorMessage);
            }

            image.IsActual = false;
            this.context.Update(image);
            await this.context.SaveChangesAsync();
        }
    }
}
