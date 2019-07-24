namespace WowGuildManager.Services.Gallery
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using WowGuildManager.Domain.Logs;

    public interface IGalleryService
    {
        Task<IEnumerable<GalleryImage>> UploadImagesAsync(List<IFormFile> files, string userId);
        Task<GalleryImage> RemoveImageAsync(string imageId);

        IEnumerable<T> GetAllImages<T>();
        T GetImage<T>(string imageId);
    }
}
