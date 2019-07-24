namespace WowGuildManager.Services.Gallery
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Common.GlobalConstants;

    public class GalleryService : IGalleryService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly Cloudinary cloudinary;
        private readonly IMapper mapper;

        public GalleryService(
            WowGuildManagerDbContext context,
            Cloudinary cloudinary,
            IMapper mapper)
        {
            this.context = context;
            this.cloudinary = cloudinary;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GalleryImage>> UploadImagesAsync(List<IFormFile> files, string userId)
        {
            var imageUploadLogs = new List<GalleryImage>();

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
                    
                    var imageUploadLog = this.GenerateImageUploadLog(uploadResult, userId);
                    imageUploadLogs.Add(imageUploadLog);
                }
            }

            await this.context.AddRangeAsync(imageUploadLogs);
            await this.context.SaveChangesAsync();

            return imageUploadLogs;
        }
        public async Task<GalleryImage> RemoveImageAsync(string imageId)
        {
            var image = this.GetImage<GalleryImage>(imageId);

            image.IsActual = false;

            this.context.Update(image);
            await this.context.SaveChangesAsync();

            return image;
        }

        public IEnumerable<T> GetAllImages<T>()
        {
            var images = this.context.GalleryImages
                .Where(i => i.IsActual)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return images;
        }
        public T GetImage<T>(string imageId)
        {
            var image = this.context.GalleryImages
                .Find(imageId);

            if (image == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidImageErrorMessage);
            }

            var mappedImage = mapper.Map<T>(image);
            return mappedImage;
        }

        private GalleryImage GenerateImageUploadLog(ImageUploadResult uploadResult, string userId)
        {
            var imageUploadLog = new GalleryImage
            {
                CreatedAt = uploadResult.CreatedAt,
                Format = uploadResult.Format,
                Length = uploadResult.Length,
                UserId = userId,
                Url = uploadResult.SecureUri.AbsoluteUri,
                IsActual = true
            };
            
            return imageUploadLog;
        }
    }
}
