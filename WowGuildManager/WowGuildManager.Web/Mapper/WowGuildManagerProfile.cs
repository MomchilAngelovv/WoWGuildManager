using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;

namespace WowGuildManager.Web.Mapper
{
    public class WowGuildManagerProfile : Profile
    {
        public WowGuildManagerProfile()
        {
            this.CreateMap<Character, CharacterViewModel>();
            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count));
        }
    }
}
