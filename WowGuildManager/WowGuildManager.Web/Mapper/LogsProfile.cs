using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Logs;
using WowGuildManager.Models.ApiModels.Logs;
using WowGuildManager.Models.ViewModels.Gallery;

namespace WowGuildManager.Web.Mapper
{
    public class LogsProfile : Profile
    {
        public LogsProfile()
        {
            this.CreateMap<GalleryImage, GalleryImageViewModel>();

            this.CreateMap<GalleryImage, ImageApiViewModel>();

            this.CreateMap<Error, ExceptionApiViewModel>();
        }
    }
}
