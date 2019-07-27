namespace WowGuildManager.Web.Mapper
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.ApiModels.Raids;
    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Guild;
    using WowGuildManager.Models.ViewModels.Characters;

    public class RaidProfile : Profile
    {
        public RaidProfile()
        {
            this.CreateMap<RaidDestination, RaidDestinationProgressApiViewModel>();
            this.CreateMap<RaidDestination, RaidDestinationProgressViewModel>();
            this.CreateMap<RaidDestination, RaidDestinationGuildMasterProgressViewModel>();

            this.CreateMap<Raid, RaidIdDestinationViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name));
            this.CreateMap<RaidDestination, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));

            this.CreateMap<Raid, RaidDetailsViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers));
             
            this.CreateMap<Raid, RaidViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Where(rc => rc.Character.IsActive).Count()))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.EventDateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<RaidCharacter, CharacterRaidDetailsViewModel>()
                .ForMember(d => d.GuildRank, dvm => dvm.MapFrom(x => x.Character.Rank.Name))
                .ForMember(d => d.Class, dvm => dvm.MapFrom(x => x.Character.Class.Name))
                .ForMember(d => d.Role, dvm => dvm.MapFrom(x => x.Character.Role.Name))
                .ForMember(d => d.Level, dvm => dvm.MapFrom(x => x.Character.Level))
                .ForMember(d => d.Name, dvm => dvm.MapFrom(x => x.Character.Name))
                .ForMember(d => d.Id, dvm => dvm.MapFrom(x => x.Character.Id));
        }
    }
}
