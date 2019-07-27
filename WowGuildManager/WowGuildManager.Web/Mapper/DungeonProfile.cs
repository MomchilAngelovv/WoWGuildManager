namespace WowGuildManager.Web.Mapper
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;

    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;

    public class DungeonProfile : Profile
    {
        public DungeonProfile()
        {
            this.CreateMap<Dungeon, DungeonIdDestinationViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name));
            this.CreateMap<DungeonDestination, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));

            this.CreateMap<Dungeon, DungeonDetailsViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers));

            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Where(rc => rc.Character.IsActive).Count()))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<DungeonCharacter, CharacterDungeonDetailsViewModel>()
                .ForMember(d => d.GuildRank, dvm => dvm.MapFrom(x => x.Character.Rank.Name))
                .ForMember(d => d.Class, dvm => dvm.MapFrom(x => x.Character.Class.Name))
                .ForMember(d => d.Role, dvm => dvm.MapFrom(x => x.Character.Role.Name))
                .ForMember(d => d.Level, dvm => dvm.MapFrom(x => x.Character.Level))
                .ForMember(d => d.Name, dvm => dvm.MapFrom(x => x.Character.Name))
                .ForMember(d => d.Id, dvm => dvm.MapFrom(x => x.Character.Id));
        }
    }
}
