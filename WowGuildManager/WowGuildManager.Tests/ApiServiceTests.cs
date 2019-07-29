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
    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Services.Api;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ApiModels.Raids;
    using WowGuildManager.Models.ApiModels.Characters;

    public class ApiServiceTests
    {
        [Fact]
        public async Task GetAllImages_Shuold_Return_All_Images()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var apiService = new ApiService(context, mapper);

            var expected = 1;
            var actual = apiService.GetAllImages().Count();

            Assert.Equal(expected, actual);

        }

        [Fact]
        public async Task GetAllExceptions_Shuold_Return_All_Exceptions()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var apiService = new ApiService(context, mapper);

            var expected = 1;
            var actual = apiService.GetAllExceptions().Count();

            Assert.Equal(expected, actual);

        }

        [Fact]
        public async Task GetCharacter_Should_Return_Correct_Character()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var apiService = new ApiService(context, mapper);

            var actual = apiService.GetCharacter("1");

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Invalid")]
        public async Task GetCharacter_Should_Throw_When_No_Character_Found(string characterId)
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var apiService = new ApiService(context, mapper);

            Assert.Throws<ArgumentException>(() => apiService.GetCharacter(characterId));
        }

        private async Task<WowGuildManagerDbContext> GetDatabase()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new WowGuildManagerDbContext(options);

            var users = new List<WowGuildManagerUser>
            {
                new WowGuildManagerUser
                {
                    Id="TestUserId1"
                }
            };
            var dungeons = new List<Dungeon>
            {
                new Dungeon
                {
                    Id="1",
                    Description = "Initial"
                }
            };
            var destinations = new List<DungeonDestination>
            {
                new DungeonDestination
                {
                    Id = "1",
                    Name = "TestDest"
                }
            };
            var characters = new List<Character>
            {
                new Character
                {
                    Id = "1",
                    UserId = "TestUserId1",
                    IsActive = true
                },
                new Character
                {
                    Id = "2",
                    UserId = "TestUserId1",
                    IsActive = true
                }
            };
            var images = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Id = "1"
                }
            };
            var exceptions = new List<Error>
            {
                new Error
                {
                    Id = "1"
                }
            };

            await context.AddRangeAsync(destinations);
            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(dungeons);
            await context.AddRangeAsync(users);
            await context.AddRangeAsync(images);
            await context.AddRangeAsync(exceptions);
            await context.SaveChangesAsync();

            return context;
        }
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Character, CharacterApiViewModel>()
                    .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name))
                    .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                    .ForMember(cvm => cvm.GuildRank, sli => sli.MapFrom(x => x.Rank.Name))
                    .ForMember(d => d.Dungeons, cvm => cvm.MapFrom(x => x.Dungeons.Select(d => d.DungeonId)))
                    .ForMember(d => d.Raids, cvm => cvm.MapFrom(x => x.Raids.Select(d => d.RaidId)));

                
                cfg.CreateMap<Error, ExceptionApiViewModel>();
                cfg.CreateMap<GalleryImage, ImageApiViewModel>();

                cfg.CreateMap<RaidDestination, RaidDestinationProgressApiViewModel>();
                cfg.CreateMap<Error, CharacterApiViewModel>();

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
