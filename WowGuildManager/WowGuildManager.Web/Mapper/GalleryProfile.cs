using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Logs;
using WowGuildManager.Models.ViewModels.Gallery;

namespace WowGuildManager.Web.Mapper
{
    public class GalleryProfile : Profile
    {
        public GalleryProfile()
        {
            this.CreateMap<ImageUploadLog, GalleryImageViewModel>();
        }
    }
}
