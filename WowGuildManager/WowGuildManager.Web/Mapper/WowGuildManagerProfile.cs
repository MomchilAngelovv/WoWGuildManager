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
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));

            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name))
                .ForMember(cvm => cvm.Rank, sli => sli.MapFrom(x => x.GuildRank.Name))
                .ForMember(cvm => cvm.Image, sli => sli.MapFrom(x => x.Class.ImagePath));

            this.CreateMap<Character, CharacterIdNameViewModel>();
            this.CreateMap<Character, CharacterNameRoleViewModel>();
            this.CreateMap<Character, CharacterDungeonDetailsViewModel>();
            this.CreateMap<Character, CharacterRaidDetailsViewModel>();

            this.CreateMap<DungeonDestination, SelectListItem>()
                 .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));

            this.CreateMap<RaidDestination, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));


            this.CreateMap<CharacterClass, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));


            this.CreateMap<CharacterRole, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));


            this.CreateMap<Character, SelectListItem>()
                .ForMember(d => d.Text, dvm => dvm.MapFrom(x => x.Name))
                .ForMember(d => d.Value, dvm => dvm.MapFrom(x => x.Id));

            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Raid, RaidViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.EventDateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Character, CharacterApiViewModel>()
                .ForMember(d => d.Dungeons, cvm => cvm.MapFrom(x => x.Dungeons.Select(d => d.DungeonId)))
                .ForMember(d => d.Raids, cvm => cvm.MapFrom(x => x.Raids.Select(d => d.RaidId)));
        }
    }
}
