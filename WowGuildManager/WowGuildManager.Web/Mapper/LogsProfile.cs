namespace WowGuildManager.Web.Mapper
{
    using AutoMapper;

    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ViewModels.Gallery;

    public class LogsProfile : Profile
    {
        public LogsProfile()
        {
            this.CreateMap<Error, ExceptionApiViewModel>();
            this.CreateMap<GalleryImage, GalleryImageViewModel>();
            this.CreateMap<GalleryImage, ImageApiViewModel>();
        }
    }
}
