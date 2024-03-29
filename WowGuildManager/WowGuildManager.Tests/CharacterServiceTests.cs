namespace WowGuildManager.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Xunit;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Character;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.BindingModels.Characters;

    public class CharacterServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Register_Character_In_Database()
        {
            using var context = await GetDatabaseAsync();
            var service = new CharacterService(context, null);

            var newCharacter = new CharacterCreateBindingModel
            {
                Class = "Rogue",
                Level = 50,
                Name = "TestChar",
                Role = "Damage",
                UserId = "TestUser1"
            };

            await service.CreateAsync(newCharacter);

            var expected = 4;
            var actual = context.Characters.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_If_User_Has_4_Registered_Characters()
        {
            using var context = await GetDatabaseAsync();
            var service = new CharacterService(context, null);

            var newCharacter = new CharacterCreateBindingModel
            {
                Class = "Rogue",
                Level = 50,
                Name = "TestChar",
                Role = "Damage",
                UserId = "TestUser1"
            };

            await service.CreateAsync(newCharacter);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateAsync(newCharacter));
        }

        [Fact]
        public async Task Delete_Should_Set_Character_Is_Active_To_False()
        {
            using var context = await GetDatabaseAsync();
            var service = new CharacterService(context, null);

            await service.DeleteAsync("1", "TestUser1");

            var expected = false;
            var actual = context.Characters.First(c => c.Id == "1").IsActive;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UserHasMaxRegiresteredCharacters_Shuold_Return_False_When_User_Has_Less_Than_4_Characters()
        {
            using var context = await GetDatabaseAsync();
            var service = new CharacterService(context, null);

            var expected = false;
            var actual = service.UserHasMaxRegiresteredCharacters("TestUser1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UserHasMaxRegiresteredCharacters_Shuold_Return_True_When_User_Has_4_Characters()
        {
            using var context = await GetDatabaseAsync();
            var service = new CharacterService(context, null);

            var newCharacter = new CharacterCreateBindingModel
            {
                Class = "Rogue",
                Level = 50,
                Name = "TestChar",
                Role = "Damage",
                UserId = "TestUser1"
            };

            await service.CreateAsync(newCharacter);

            var expected = true;
            var actual = service.UserHasMaxRegiresteredCharacters("TestUser1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetClassId_Should_Throw_If_No_Such_Class_Found()
        {
            using var context = await this.GetDatabaseAsync();

            var characterService = new CharacterService(context, null);
            Assert.Throws<ArgumentException>(() => characterService.GetClassId("InvalidClass")); 
        }

        [Fact]
        public async Task GetRoleId_Should_Throw_If_No_Such_Role_Found()
        {
            using var context = await this.GetDatabaseAsync();

            var characterService = new CharacterService(context, null);
            Assert.Throws<ArgumentException>(() => characterService.GetRoleId("InvalidRole"));
        }

        [Fact]
        public async Task GetRankId_Should_Throw_If_No_Such_Rank_Found()
        {
            using var context = await this.GetDatabaseAsync();

            var characterService = new CharacterService(context, null);
            Assert.Throws<ArgumentException>(() => characterService.GetRankId("InvalidRole"));
        }

        private async Task<WowGuildManagerDbContext> GetDatabaseAsync()
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

            var context = new WowGuildManagerDbContext(options);

            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(classes);
            await context.AddRangeAsync(roles);
            await context.AddRangeAsync(ranks);
            await context.SaveChangesAsync();

            return context;
        }
    }
}
