namespace WowGuildManager.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Xunit;
    using AutoMapper;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Services.Gallery;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ViewModels.Gallery;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;

    public class GalleryServiceTests
    {
        [Fact]
        public async Task RemoveImageAsync_Should_Set_IsActual_To_False()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var galleryService = new GalleryService(context, null, mapper);

            await galleryService.RemoveImageAsync("1");

            var expected = false;
            var actual = (await context.GalleryImages.FirstAsync()).IsActual;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetImage_Should_Return_Correct_Image()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var galleryService = new GalleryService(context, null, mapper);

            var expected = "1";
            var actual = galleryService.GetImage<ImageApiViewModel>("1").Id;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetImage_Should_Throw_If_No_Image_Found()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var galleryService = new GalleryService(context, null, mapper);

            
            Assert.Throws<ArgumentException>(() => galleryService.GetImage<ImageApiViewModel>("Incorrect").Id);
        }

        private async Task<WowGuildManagerDbContext> GetDatabase()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString())
             .Options;

            var characters = new List<Character>
            {
                new Character
                {
                    Id = "1",
                    UserId = "TestUser1",
                    IsActive = true
                },
                new Character
                {
                     UserId = "TestUser1",
                      IsActive = true
                },
                new Character
                {
                     UserId = "TestUser1",
                      IsActive = true
                },
            };
            var classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Id = "1",
                    Name = "Rogue"
                }
            };
            var roles = new List<CharacterRole>
            {
                new CharacterRole
                {
                    Id = "1",
                    Name = "Damage"
                },
                new CharacterRole
                {
                    Id = "2",
                    Name = "Tank"
                }
            };
            var ranks = new List<CharacterRank>
            {
                new CharacterRank
                {
                    Id = "1",
                    Name = "Member"
                }
            };
            var galleryImages = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Id = "1",
                    IsActual=true
                }
            };

            var context = new WowGuildManagerDbContext(options);

            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(classes);
            await context.AddRangeAsync(roles);
            await context.AddRangeAsync(ranks);
            await context.AddRangeAsync(galleryImages);
            await context.SaveChangesAsync();

            return context;
        }
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Dungeon, DungeonIdDestinationViewModel>()
                .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name));

                cfg.CreateMap<Dungeon, DungeonDetailsViewModel>()
                    .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                    .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers));

                cfg.CreateMap<Dungeon, DungeonViewModel>()
                    .ForMember(d => d.RegisteredPlayers, dvm => dvm.MapFrom(x => x.RegisteredCharacters.Where(rc => rc.Character.IsActive).Count()))
                    .ForMember(d => d.Image, dvm => dvm.MapFrom(x => x.Destination.ImagePath))
                    .ForMember(d => d.MaxPlayers, dvm => dvm.MapFrom(x => x.Destination.MaxPlayers))
                    .ForMember(d => d.Destination, dvm => dvm.MapFrom(x => x.Destination.Name))
                    .ForMember(d => d.DateTime, dvm => dvm.MapFrom(x => $"{x.EventDateTime.ToString("dd MMMM yyyy HH:mm")}"));

                cfg.CreateMap<DungeonCharacter, CharacterDungeonDetailsViewModel>()
                    .ForMember(d => d.GuildRank, dvm => dvm.MapFrom(x => x.Character.Rank.Name))
                    .ForMember(d => d.Class, dvm => dvm.MapFrom(x => x.Character.Class.Name))
                    .ForMember(d => d.Role, dvm => dvm.MapFrom(x => x.Character.Role.Name))
                    .ForMember(d => d.Level, dvm => dvm.MapFrom(x => x.Character.Level))
                    .ForMember(d => d.Name, dvm => dvm.MapFrom(x => x.Character.Name))
                    .ForMember(d => d.Id, dvm => dvm.MapFrom(x => x.Character.Id));

                cfg.CreateMap<Error, ExceptionApiViewModel>();
                cfg.CreateMap<GalleryImage, GalleryImageViewModel>();
                cfg.CreateMap<GalleryImage, ImageApiViewModel>();

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
