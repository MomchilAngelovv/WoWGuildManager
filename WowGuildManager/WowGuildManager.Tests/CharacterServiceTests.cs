namespace WowGuildManager.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Xunit;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.BindingModels.Characters;

    public class CharacterServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Register_Character_In_Database()
        {
            var context = await GetDatabase();
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
            var context = await GetDatabase();
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
        public async Task Delete_Should_Remove_Character_From_Database()
        {
            var context = await GetDatabase();
            var service = new CharacterService(context, null);

            await service.Delete("1");

            var expected = 2;
            var actual = context.Characters.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UserHasMaxRegiresteredCharacters_Shuold_Return_False_When_User_Has_Less_Than_4_Characters()
        { 
            var context = await GetDatabase();
            var service = new CharacterService(context, null);

            var expected = false;
            var actual = service.UserHasMaxRegiresteredCharacters("TestUser1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UserHasMaxRegiresteredCharacters_Shuold_Return_True_When_User_Has_4_Characters()
        {
            var context = await GetDatabase();
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
                    UserId = "TestUser1"
                },
                new Character
                {
                     UserId = "TestUser1"
                },
                new Character
                {
                     UserId = "TestUser1"
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
            var ranks = new List<GuildRank>
            {
                new GuildRank
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
