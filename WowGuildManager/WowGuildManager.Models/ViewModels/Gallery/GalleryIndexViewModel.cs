namespace WowGuildManager.Models.ViewModels.Gallery
{
    using System.Collections.Generic;

    public class GalleryIndexViewModel
    {
        public IEnumerable<GalleryImageViewModel> Images { get; set; }
    }
}
