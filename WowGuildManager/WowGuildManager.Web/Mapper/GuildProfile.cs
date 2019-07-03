namespace WowGuildManager.Web.Mapper
{
    using AutoMapper;

    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Models.ViewModels.Users;

    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            this.CreateMap<WowGuildManagerUser, UserAdminViewModel>()
                .ForMember(cvm => cvm.Id, sli => sli.MapFrom(x => x.Id))
                .ForMember(cvm => cvm.Name, sli => sli.MapFrom(x => x.UserName));
        }
    }
}
