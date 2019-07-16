using System.Collections.Generic;

namespace WowGuildManager.Models.ViewModels.Gallery
{
    public class GalleryIndexViewModel
    {
        public IEnumerable<GalleryImageViewModel> Images { get; set; }
    }
}
