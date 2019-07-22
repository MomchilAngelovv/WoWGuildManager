namespace WowGuildManager.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Xunit;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Services.Dungeons;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Models.BindingModels.Dungeons;

    public class DungeonServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Register_Dungeon_In_Database()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                var dungeonCreateBindingModel = new DungeonCreateBindingModel
                {
                    DateTime = DateTime.Now,
                    Description = "TestDescr",
                    Destination = "TestDest",
                    LeaderId = "1"
                };

                await dungeonService.CreateAsync(dungeonCreateBindingModel);

                var expected = 2;
                var actual = context.Dungeons.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_If_Incorrect_Destination_Provided()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                var dungeonCreateBindingModel = new DungeonCreateBindingModel
                {
                    DateTime = DateTime.Now,
                    Description = "TestDescr",
                    Destination = "TestDest2",
                    LeaderId = "TestChar1"
                };

                await Assert.ThrowsAsync<ArgumentException>(async () => await dungeonService.CreateAsync(dungeonCreateBindingModel));
            }
        }

        [Fact]
        public async Task RegisterCharacter_Should_Create_New_Dungeon_Character_Entity_In_Database()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await dungeonService.RegisterCharacterAsync("1", "1");
                var expected = 1;
                var actual = context.DungeonCharacter.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task RegisterCharacter_Should_Throw_If_No_Character_Found()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await Assert.ThrowsAsync<ArgumentException>(async () => await dungeonService.RegisterCharacterAsync("IncorectId", "1"));
            }
        }

        [Fact]
        public async Task GetDestinationId_Should_Return_Correct_Id()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                var expected = "1";
                var actual = dungeonService.GetDestinationIdByName("TestDest");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task GetDestinationId_Should_Throw_If_No_Such_Destination_Exists()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                Assert.Throws<ArgumentException>(() => dungeonService.GetDestinationIdByName("Incorrect"));
            }
        }

        [Fact]
        public async Task RegisterCharacter_Should_Throw_If_No_Dungeon_Found()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await Assert.ThrowsAsync<ArgumentException>(async () => await dungeonService.RegisterCharacterAsync("1", "IncorectId"));
            }
        }

        [Fact]
        public async Task KickCharacter_Should_Remove_Dungeon_Character_Entity_In_Database()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await dungeonService.RegisterCharacterAsync("1", "1");
                await dungeonService.KickCharacter("1", "1");

                var expected = 0;
                var actual = context.DungeonCharacter.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task KickCharacter_Should_Throw_If_Invalid_Character()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await Assert.ThrowsAsync<InvalidOperationException>(async () => await dungeonService.KickCharacter("Incorrect", "1"));
            }
        }

        [Fact]
        public async Task KickCharacter_Should_Throw_If_Invalid_Dungeon()
        {
            using (var context = await GetDatabase())
            {
                var dungeonService = new DungeonService(context, null);

                await Assert.ThrowsAsync<InvalidOperationException>(async () => await dungeonService.KickCharacter("1", "Incorrect"));
            }
        }


        private async Task<WowGuildManagerDbContext> GetDatabase()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new WowGuildManagerDbContext(options);

            var dungeons = new List<Dungeon>
            {
                new Dungeon
                {
                    Id="1",
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
                    Id = "1"
                }
            };

            await context.AddRangeAsync(destinations);
            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(dungeons);
            await context.SaveChangesAsync();

            return context;
        }
    }
}
