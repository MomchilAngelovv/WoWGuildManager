using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Users;

namespace WowGuildManager.Web.Mapper
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<WowGuildManagerUser, UserAdminViewModel>()
                .ForMember(cvm => cvm.Id, sli => sli.MapFrom(x => x.Id))
                .ForMember(cvm => cvm.Name, sli => sli.MapFrom(x => x.UserName));
        }
    }
}
