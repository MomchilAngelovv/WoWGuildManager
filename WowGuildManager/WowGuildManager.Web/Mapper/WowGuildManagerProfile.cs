using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Web.Mapper
{
    public class WowGuildManagerProfile : Profile
    {
        public WowGuildManagerProfile()
        {
            this.CreateMap<Character, CharacterViewModel>();

            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.DateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Raid, RaidViewModel>()
               .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
               .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.DateTime.ToString("dd MMMM yyyy HH:mm")}"));

        }
    }
}
