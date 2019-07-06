namespace WowGuildManager.Web.Mapper
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Models.ViewModels.Dungeons;

    public class DungeonProfile : Profile
    {
        public DungeonProfile()
        {
            this.CreateMap<Dungeon, DungeonViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Dungeon, DungeonIdDestinationViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name));

            this.CreateMap<Dungeon, DungeonDetailsViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers));

            this.CreateMap<DungeonDestination, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));
        }
    }
}
