using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Domain.Logs;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ApiModels.Logs;
using WowGuildManager.Models.BindingModels.Characters;
using WowGuildManager.Models.BindingModels.Raids;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Gallery;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Raids;
using Xunit;

namespace WowGuildManager.Tests
{
    public class RaidServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Create_And_Push_Raid_In_Database()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            var raidCreateBindingModel = new RaidCreateBindingModel
            {
                DateTime = DateTime.Now,
                Description = "TestDescr",
                Destination = "TestRaid1",
                LeaderId = "1"
            };

            await raidService.CreateAsync(raidCreateBindingModel);

            var expected = 2;
            var actual = context.Raids.Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("Invalid", null)]
        [InlineData(null, "Invalid")]
        [InlineData("Invalid", "Invalid")]
        public async Task CreateAsync_Should_Throw_If_Not_Valid_InputData(string destination, string leaderId)
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            var raidCreateBindingModel = new RaidCreateBindingModel
            {
                DateTime = DateTime.Now,
                Destination = destination,
                LeaderId = leaderId
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await raidService.CreateAsync(raidCreateBindingModel));
        }

        [Theory]
        [InlineData("New Descr")]
        [InlineData("Correct")]
        public async Task EditAsync_Should_Edit_Properly(string description)
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            var raidEditBindingModel = new RaidEditBindingModel
            {
                Description = description,
                EventDateTime = DateTime.Now,
                RaidId = "1"
            };

            await raidService.EditAsync(raidEditBindingModel);

            var expected = description;
            var actual = context.Raids.First(r => r.Id == "1").Description;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RegisterCharacterAsync_Should_Register_Character_To_Given_Raid()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            await raidService.RegisterCharacterAsync("1","1");

            var expected = 1;
            var actual = context.Raids.First(r => r.Id == "1").RegisteredCharacters.Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null,null)]
        [InlineData("Incorrect", null)]
        [InlineData("1", "Incorect")]
        [InlineData("Incorect", "1")]
        public async Task RegisterCharacterAsync_Should_Throw_If_Character_Or_Raid_Not_Found(string characterId, string riadId)
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            await Assert.ThrowsAsync<ArgumentException>(async() => await raidService.RegisterCharacterAsync(characterId, riadId));
        }

        [Fact]
        public async Task KickCharacterAsync_Should_Remove_Character_From_Raid()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            await raidService.RegisterCharacterAsync("1", "1");
            await raidService.KickPlayerAsync("1","1");

            var expected = 0;
            var actual = context.RaidCharacter.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task KickCharacterAsync_Should_Throw_If_No_Such_Character_Joined_Raid()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            await Assert.ThrowsAsync<InvalidOperationException>(async() => await raidService.KickPlayerAsync("1", "1"));
        }

        [Fact]
        public async Task GetRaid_Should_Return_Correct_Raid()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            var expected = "Correct";
            var actual = raidService.GetRaid<Raid>("1").Description;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetRaid_Should_Throw_If_No_Raid_Found()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            Assert.Throws<ArgumentException>(() => raidService.GetRaid<Raid>("Invalid"));
        }

        [Fact]
        public async Task GetDestination_Should_Return_Correct_Destination()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            var expected = 10;
            var actual = raidService.GetDestination<RaidDestination>("TestRaid1").TotalBosses;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetDestination_Should_Throw_If_No_Destination_Found()
        {
            var context = await this.GetDatabase();
            var mapper = this.GetMapper();
            var characterService = new CharacterService(context, mapper);
            var raidService = new RaidService(context, characterService, mapper);

            Assert.Throws<ArgumentException>(() => raidService.GetDestination<RaidDestination>("Invalid"));
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
            var raids = new List<Raid>
            {
                new Raid
                {
                    Id = "1",
                    Description = "Correct"
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
            await context.AddRangeAsync(raids);
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
