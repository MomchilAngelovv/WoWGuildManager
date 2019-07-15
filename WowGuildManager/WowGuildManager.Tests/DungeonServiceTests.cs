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
        public async Task CreateAsync_Should_Register_Character()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase("CreateAsync_Database")
                .Options;

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var dungeonService = new DungeonService(dbContext, null);

                var dungeonDestination = new DungeonDestination
                {
                    Name = "Scarlet Monastery"
                };

                dbContext.DungeonDestinations.Add(dungeonDestination);
                dbContext.SaveChanges();

                var dungeonCreateBindingModel = new DungeonCreateBindingModel
                {
                    DateTime = DateTime.Now,
                    Description = "RandomStuff",
                    Destination = "Scarlet Monastery",
                    LeaderId = "TestUser1"
                };

                await dungeonService.CreateAsync(dungeonCreateBindingModel);

                var expectedResult = 1;
                var actualResult = dbContext.Dungeons.Count();

                Assert.Equal(expectedResult, actualResult);
            }
        }

        [Fact]
        public async Task RegisterCharacterAsync_Should_Register_Character_To_Dungeon()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
               .UseInMemoryDatabase("RegisterCharacterAsync_Database")
               .Options;

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var dungeonService = new DungeonService(dbContext, null);

                var dungeon = new Dungeon
                {
                    Id = "D1"
                };

                var character = new Character
                {
                    Id = "C1"
                };

                dbContext.Dungeons.Add(dungeon);
                dbContext.Characters.Add(character);
                dbContext.SaveChanges();

                await dungeonService.RegisterCharacterAsync(character.Id, dungeon.Id);

                var expectedResult = 1;
                var actualResult = dbContext.DungeonCharacter.Count();

                Assert.Equal(expectedResult, actualResult);
            }
        }

        [Fact]
        public void RegisterCharacterAsync_Should_Throw_If_No_Character_Or_Dungeon_Found()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
               .UseInMemoryDatabase("RegisterCharacterAsync_Database")
               .Options;

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var dungeonService = new DungeonService(dbContext, null);

                Assert.Throws<ArgumentException>(() => dungeonService.RegisterCharacterAsync("NoSuchCharacterId", "NoSuchDungeonId").GetAwaiter().GetResult());
            }
        }

        [Fact]
        public void GetDestinationByDestinationName_Should_Return_Correct_Destination()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
              .UseInMemoryDatabase("RegisterCharacterAsync_Database")
              .Options;

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var dungeonService = new DungeonService(dbContext, null);

                var dungeonDestination = new DungeonDestination
                {
                    Id = "1",
                    Name = "TestDestination"
                };
                dbContext.DungeonDestinations.Add(dungeonDestination);
                dbContext.SaveChanges();

                var expectedResult = "1";
                var actualResult = dungeonService.GetDestinationIdByName("TestDestination");

                Assert.Equal(expectedResult, actualResult);
            }
        }

        [Fact]
        public async Task KickPlayer_Should_Properly_Delete_Registered_Player_From_Dungeon()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase("KickCharacter_Database")
                .Options;

            using (var context = new WowGuildManagerDbContext(options))
            {
                var dungeonService = new DungeonService(context, null);

                var dungeonCharacters = new List<DungeonCharacter>
                {
                     new DungeonCharacter
                     {
                         CharacterId = "TestCharacterId1",
                         DungeonId = "TestDungeonId1"
                     },
                     new DungeonCharacter
                     {
                         CharacterId = "TestCharacterId2",
                         DungeonId = "TestDungeonId2"
                     },
                };

                await context.DungeonCharacter.AddRangeAsync(dungeonCharacters);
                await context.SaveChangesAsync();

                await dungeonService.KickCharacter("TestCharacterId1", "TestDungeonId1");

                var expectedCount = 1;

                Assert.Equal(expectedCount, context.DungeonCharacter.Count());
            }
        }
    }
}
