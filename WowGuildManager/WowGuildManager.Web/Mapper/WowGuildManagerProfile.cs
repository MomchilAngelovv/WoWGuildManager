using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ApiModels.Characters;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Web.Mapper
{
    public class WowGuildManagerProfile : Profile
    {
        public WowGuildManagerProfile()
        {
            this.CreateMap<CharacterClass, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.ToString()))
                .ForMember(cl => cl.Value, sli => sli.MapFrom(x => ((int)x).ToString()));

            this.CreateMap<Character, CharacterViewModel>();

            this.CreateMap<Character, CharacterIdNameViewModel>();
            this.CreateMap<Character, CharacterNameRoleViewModel>();
            this.CreateMap<Character, CharacterDungeonDetailsViewModel>();
            
            this.CreateMap<Character, SelectListItem>()
                .ForMember(d => d.Text, dvm => dvm.MapFrom(x => x.Name))
                .ForMember(d => d.Value, dvm => dvm.MapFrom(x => x.Id));

            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.DateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Raid, RaidViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.DateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Character, CharacterApiViewModel>()
                .ForMember(d => d.Dungeons, cvm => cvm.MapFrom(x => x.Dungeons.Select(d => d.DungeonId)))
                .ForMember(d => d.Raids, cvm => cvm.MapFrom(x => x.Raids.Select(d => d.RaidId)));
        }
    }
}
