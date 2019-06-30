using System;
using WowGuildManager.Services.Characters;
using Xunit;
using Moq;
using WowGuildManager.Domain.Characters;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WowGuildManager.Data;

namespace WowGuildManager.Tests
{
    public class CharacterServiceTests
    {
        [Fact]
        public void GetRoles_Should_Return_Correct_Roles()
        {
           
        }

        [Fact]
        public void GetRoleIdByName_Should_Return_Correct_Id()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoleIdByName_Database")
                .Options;

            var roles = new List<CharacterRole>
            {
                new CharacterRole
                {
                    Id = "1",
                    Name = "Tank"
                },
                new CharacterRole
                {
                    Id = "2",
                    Name = "Healer"
                },
                new CharacterRole
                {
                    Id = "3",
                    Name = "Damage"
                },
            };

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var characterService = new CharacterService(dbContext, null);
                dbContext.CharacterRoles.AddRange(roles);
                dbContext.SaveChanges();

                var actualRoleId = characterService.GetRoleIdByName("Damage");
                Assert.Equal("3", actualRoleId);
            }
        }

        [Fact]
        public void GetRankIdByName_Should_Return_Correct_Id()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRankIdByName_Database")
                .Options;

            var ranks = new List<GuildRank>
            {
                new GuildRank
                {
                    Id = "1",
                    Name = "Member"
                },
                new GuildRank
                {
                    Id = "2",
                    Name = "GuildMaster"
                },
                new GuildRank
                {
                    Id = "3",
                    Name = "PvP"
                },
            };

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var characterService = new CharacterService(dbContext, null);
                dbContext.GuildRanks.AddRange(ranks);
                dbContext.SaveChanges();

                var actualRankId = characterService.GetRankIdByName("GuildMaster");
                Assert.Equal("2", actualRankId);
            }
        }
    }
}
