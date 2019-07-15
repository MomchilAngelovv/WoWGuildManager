namespace WowGuildManager.Web.Mapper
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.ApiModels.Raids;
    using WowGuildManager.Models.ViewModels.Guild;
    using WowGuildManager.Models.ViewModels.Raids;

    public class RaidProfile : Profile
    {
        public RaidProfile()
        {
            this.CreateMap<Raid, RaidViewModel>()
                .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Count))
                .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                .ForMember(d => d.EventDateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

            this.CreateMap<Raid, RaidIdDestinationViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name));

            this.CreateMap<Raid, RaidDetailsViewModel>()
               .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
               .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers));

            this.CreateMap<RaidDestination, RaidDestinationProgressApiViewModel>();
           
            this.CreateMap<RaidDestination, RaidDestinationProgressViewModel>();

            this.CreateMap<RaidDestination, RaidDestinationGuildMasterProgressViewModel>();

            this.CreateMap<RaidDestination, SelectListItem>()
                .ForMember(cl => cl.Text, sli => sli.MapFrom(x => x.Name));
        }
    }
}
