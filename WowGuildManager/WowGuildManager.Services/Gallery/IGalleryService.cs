using System.Collections.Generic;
using System.Threading.Tasks;

namespace WowGuildManager.Services.Gallery
{
    public interface IGalleryService
    {
        IEnumerable<T> GetAllGallery<T>();

        Task MakeNonActualAsync(string imageId);
    }
}
