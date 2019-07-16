using System.Collections.Generic;

namespace WowGuildManager.Services.Gallery
{
    public interface IGalleryService
    {
        IEnumerable<string> GetAllGallery();
    }
}
