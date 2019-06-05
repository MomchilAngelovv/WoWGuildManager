using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Web.Mapper
{
    public class WowGuildManagerProfile : Profile
    {
        public WowGuildManagerProfile()
        {
            this.CreateMap<Character, CharacterViewModel>();
        }
    }
}
