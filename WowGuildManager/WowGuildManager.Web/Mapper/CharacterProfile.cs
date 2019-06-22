using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Models.ApiModels.Characters;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Web.Mapper
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name))
                .ForMember(cvm => cvm.Rank, sli => sli.MapFrom(x => x.GuildRank.Name))
                .ForMember(cvm => cvm.Image, sli => sli.MapFrom(x => x.Class.ImagePath));

            this.CreateMap<Character, CharacterIdNameViewModel>();
            this.CreateMap<Character, CharacterNameRoleViewModel>();
            this.CreateMap<Character, CharacterDungeonDetailsViewModel>();
            this.CreateMap<Character, CharacterRaidDetailsViewModel>();

            this.CreateMap<Character, CharacterApiViewModel>()
                .ForMember(d => d.Dungeons, cvm => cvm.MapFrom(x => x.Dungeons.Select(d => d.DungeonId)))
                .ForMember(d => d.Raids, cvm => cvm.MapFrom(x => x.Raids.Select(d => d.RaidId)));

            this.CreateMap<Character, SelectListItem>()
                .ForMember(d => d.Text, dvm => dvm.MapFrom(x => x.Name))
                .ForMember(d => d.Value, dvm => dvm.MapFrom(x => x.Id));

            this.CreateMap<CharacterClass, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));

            this.CreateMap<CharacterRole, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));
        }
    }
}
