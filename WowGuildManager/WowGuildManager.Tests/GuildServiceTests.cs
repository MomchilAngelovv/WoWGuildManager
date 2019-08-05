namespace WowGuildManager.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Xunit;
    using AutoMapper;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Services.Raids;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Guilds;
    using WowGuildManager.Domain.Character;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ViewModels.Gallery;
    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Characters;

    public class GuildServiceTests
    {
        [Fact]
        public async Task PromoteRankAsync_Should_Increase_Rank_Correctly()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.PromoteRankAsync("1");

            var expected = "2";
            var actual = context.Characters.First(ch => ch.Id == "1").RankId;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async Task DemoteRank_Should_Decrease_Rank_Correctly()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.DemoteRankAsync("2");

            var expected = "1";
            var actual = context.Characters.First(ch => ch.Id == "2").RankId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddProgressToRaid_Should_Increase_Killed_Bosses_By_1()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.AddProgressToRaidAsync("TestRaid1");

            var expected = 1;
            var actual = context.RaidDestinations.First(ch => ch.Id == "1").KilledBosses;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddProgressToRaid_Should_Not_Increase_When_Killed_Bosses_Equal_TotalBosses()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.AddProgressToRaidAsync("TestRaid2");

            var expected = 10;
            var actual = context.RaidDestinations.First(ch => ch.Id == "2").KilledBosses;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RemoveProgressToRaid_Should_Decrese_Killed_Bosses_By_1()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.RemoveProgressToRaidAsync("TestRaid2");

            var expected = 9;
            var actual = context.RaidDestinations.First(ch => ch.Id == "2").KilledBosses;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RemoveProgressToRaid_Should_Not_Decrese_If_Killed_Bosses_Equal_0()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            await guildService.RemoveProgressToRaidAsync("TestRaid1");

            var expected = 0;
            var actual = context.RaidDestinations.First(ch => ch.Id == "1").KilledBosses;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTotalRegisteredUsersCount_Should_Return_Proper_Count()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);

            var expected = 1;
            var actual = guildService.GetTotalRegisteredUsersCount();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTotalRegisteredCharactersCount_Should_Return_Proper_Count()
        {
            using var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);
            var guildService = new GuildService(null, context, raidService, characterService, mapper);
            
            var expected = 3;
            var actual = guildService.GetTotalRegisteredCharactersCount();

            Assert.Equal(expected, actual);
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
                    IsActive = true,
                    RankId = "1",
                },
                new Character
                {
                    Id = "2",
                    UserId = "TestUser1",
                    IsActive = true,
                    RankId = "2"
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
                },
                new CharacterRank
                {
                    Id = "2",
                    Name = "Alt"
                }
            };
            var galleryImages = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Id = "1",
                    IsActual = true,
                }
            };
            var raidDestinations = new List<RaidDestination>
            {
                new RaidDestination
                {
                    Id= "1",
                    Name = "TestRaid1",
                    KilledBosses = 0,
                    TotalBosses = 10
                },
                new RaidDestination
                {
                    Id= "2",
                    Name = "TestRaid2",
                    KilledBosses = 10,
                    TotalBosses = 10
                }
            };
            var users = new List<WowGuildManagerUser>
            {
                new WowGuildManagerUser
                {
                    Id= "1",
                }
            };

            var context = new WowGuildManagerDbContext(options);

            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(classes);
            await context.AddRangeAsync(roles);
            await context.AddRangeAsync(ranks);
            await context.AddRangeAsync(galleryImages);
            await context.AddRangeAsync(raidDestinations);
            await context.AddRangeAsync(users);
            await context.SaveChangesAsync();

            return context;
        }
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
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
