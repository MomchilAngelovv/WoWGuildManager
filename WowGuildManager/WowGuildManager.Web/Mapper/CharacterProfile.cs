namespace WowGuildManager.Web.Mapper
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;

    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.BindingModels.Characters;

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

            this.CreateMap<Character, CharacterEditBindingModel>()
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name));

            this.CreateMap<Character, CharacterDungeonDetailsViewModel>()
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                .ForMember(cvm => cvm.GuildRank, sli => sli.MapFrom(x => x.GuildRank.Name))
                .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name));

            this.CreateMap<Character, CharacterRaidDetailsViewModel>()
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                .ForMember(cvm => cvm.GuildRank, sli => sli.MapFrom(x => x.GuildRank.Name))
                .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name));

            this.CreateMap<Character, CharacterApiViewModel>()
                .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name))
                .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                .ForMember(cvm => cvm.GuildRank, sli => sli.MapFrom(x => x.GuildRank.Name))
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
