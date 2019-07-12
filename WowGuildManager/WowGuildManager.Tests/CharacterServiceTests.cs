//TODO: Make last all tables same design (same classes templete)
//TODO: MAKE ALL TESTS AGAIN WITH COMMON DB AND EAZIER TO FIX
namespace WowGuildManager.Tests
{
    using Xunit;
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Models.ViewModels.Characters;
    using WowGuildManager.Models.BindingModels.Characters;
    using System.Threading.Tasks;

    public class CharacterServiceTests
    {
        [Fact]
        public async Task CreatAsync_Should_Register_New_Character()
        {
            using (var context = await this.GenerateDatabase())
            {
                var characterService = new CharacterService(context, null);

                var newUser = new CharacterCreateBindingModel
                {
                    Class = "Rogue",
                    Level = 60,
                    Name = "TestName1",
                    Role = "Damage",
                    UserId = "TestUser2"
                };

                await characterService.CreateAsync(newUser);

                var expected = 4;
                var actual = context.Characters.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task CreatAsync_Should_Throw_If_User_Has_4_Registered_Characters()
        {
            using (var context = await this.GenerateDatabase())
            {
                var characterService = new CharacterService(context, null);

                var newUser = new CharacterCreateBindingModel
                {
                    Class = "Rogue",
                    Level = 60,
                    Name = "TestName1",
                    Role = "Damage",
                    UserId = "TestUserId1",
                };

                await characterService.CreateAsync(newUser);

                await Assert.ThrowsAsync<InvalidOperationException>(async () => await characterService.CreateAsync(newUser));
            }
        }

        private async Task<WowGuildManagerDbContext> GenerateDatabase()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new WowGuildManagerDbContext(options);

            var characters = new List<Character>
            {
                new Character
                {
                    WowGuildManagerUserId = "TestUserId1"
                },
                new Character
                {
                    WowGuildManagerUserId = "TestUserId1"
                },
                new Character
                {
                    WowGuildManagerUserId = "TestUserId1"
                },
            };
            var classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Rogue"
                }
            };
            var roles = new List<CharacterRole>
            {
                 new CharacterRole
                 {
                     Name = "Damage"
                 }
            };
            var ranks = new List<GuildRank>
            {
                new GuildRank
                {
                    Name = "Member"
                }
            };

            await context.AddRangeAsync(classes);
            await context.AddRangeAsync(roles);
            await context.AddRangeAsync(characters);
            await context.AddRangeAsync(ranks);

            await context.SaveChangesAsync();

            return context;
        }
    }
}
