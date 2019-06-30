using System;
using WowGuildManager.Services.Characters;
using Xunit;
using Moq;
using WowGuildManager.Domain.Characters;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WowGuildManager.Data;
using WowGuildManager.Models.ViewModels.Characters;
using AutoMapper;

//TODO: Check how to compare List<T> in XunitTests
namespace WowGuildManager.Tests
{
    public class CharacterServiceTests
    {
        [Fact]
        public void GetRoles_Should_Return_Correct_Roles()
        {

        }

        [Fact]
        public void GetRoleIdByName_Should_Theow_When_Not_Found()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRoleIdByName_Throw_Database")
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

                Assert.Throws<ArgumentException>(() => characterService.GetRoleIdByName("NoSuchRole"));
            }
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
        public void GetRankIdByName_Should_Throw_When_Not_Found()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetRankIdByName_Throw_Database")
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

                Assert.Throws<ArgumentException>(() => characterService.GetRankIdByName("NoSuchRole"));
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

        [Fact]
        public void GetClassIdByName_Should_Throw_When_Not_Found()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetClassIdByName_Throw_Database")
                .Options;

            var classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Id = "1",
                    Name = "Rogue"
                },
                new CharacterClass
                {
                    Id = "2",
                    Name = "Warrior"
                },
                new CharacterClass
                {
                    Id = "3",
                    Name = "Priest"
                },
            };

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var characterService = new CharacterService(dbContext, null);
                dbContext.CharacterClasses.AddRange(classes);
                dbContext.SaveChanges();

                Assert.Throws<ArgumentException>(() => characterService.GetClassIdByName("NoSuchClass"));
            }
        }

        [Fact]
        public void GetClassIdByName_Should_Return_Correct_Id()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetClassIdByName_Database")
                .Options;

            var classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Id = "1",
                    Name = "Rogue"
                },
                new CharacterClass
                {
                    Id = "2",
                    Name = "Warrior"
                },
                new CharacterClass
                {
                    Id = "3",
                    Name = "Priest"
                },
            };

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var characterService = new CharacterService(dbContext, null);
                dbContext.CharacterClasses.AddRange(classes);
                dbContext.SaveChanges();

                var actualClassId = characterService.GetClassIdByName("Rogue");
                Assert.Equal("1", actualClassId);
            }
        }

        [Fact]
        public void GetAll_Should_Return_Correctly_Mapped_Collections()
        {
            var options = new DbContextOptionsBuilder<WowGuildManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "GetClassIdByName_Database")
                .Options;

            var config = new MapperConfiguration(options =>
            {
                options.CreateMap<Character, CharacterViewModel>()
                    .ForMember(cvm => cvm.Role, sli => sli.MapFrom(x => x.Role.Name))
                    .ForMember(cvm => cvm.Class, sli => sli.MapFrom(x => x.Class.Name))
                    .ForMember(cvm => cvm.Rank, sli => sli.MapFrom(x => x.GuildRank.Name))
                    .ForMember(cvm => cvm.Image, sli => sli.MapFrom(x => x.Class.ImagePath));
            });

            var mapper = config.CreateMapper();

            var characters = new List<Character>
            {
                new Character
                {
                    Id = "1",
                    Name = "Misty",
                    Level = 60,
                    Role = new CharacterRole
                    {
                        Name = "Tank"
                    },
                    Class = new CharacterClass
                    {
                        Name = "Warrior",
                        ImagePath = "TestImg1"
                    },
                    GuildRank = new GuildRank
                    {
                        Name = "GuildMaster"
                    },
                },

                new Character
                {
                    Id = "2",
                    Name = "Himmel",
                    Level = 55,
                    Role = new CharacterRole
                    {
                        Name = "Healer"
                    },
                    Class = new CharacterClass
                    {
                        Name = "Paladin",
                        ImagePath = "TestImg2"
                    },
                    GuildRank = new GuildRank
                    {
                        Name = "Member"
                    },
                }
            };

            using (var dbContext = new WowGuildManagerDbContext(options))
            {
                var characterService = new CharacterService(dbContext, mapper);
                dbContext.Characters.AddRange(characters);
                dbContext.SaveChanges();

                var expecterAllCharacters = new List<CharacterViewModel>
                {

                    new CharacterViewModel
                    {
                        Id = "1",
                        Class = "Warrior",
                        Name = "Misty",
                        Level = "60",
                        Image = "TestImg1",
                        Rank = "GuildMaster",
                        Role = "Tank"

                    },
                    new CharacterViewModel
                    {
                        Id = "2",
                        Class = "Paladin",
                        Name = "Himmel",
                        Level = "55",
                        Image = "TestImg2",
                        Rank = "Member",
                        Role = "Healer"
                    },

                };
                var actualAllCharacters = characterService.GetAll<CharacterViewModel>().ToList();

                for (int index = 0; index < expecterAllCharacters.Count; index++)
                {
                    Assert.Equal(expecterAllCharacters[index].Id,actualAllCharacters[index].Id);
                    Assert.Equal(expecterAllCharacters[index].Class, actualAllCharacters[index].Class);
                    Assert.Equal(expecterAllCharacters[index].Name, actualAllCharacters[index].Name);
                    Assert.Equal(expecterAllCharacters[index].Level, actualAllCharacters[index].Level);
                    Assert.Equal(expecterAllCharacters[index].Image, actualAllCharacters[index].Image);
                    Assert.Equal(expecterAllCharacters[index].Rank, actualAllCharacters[index].Rank);
                    Assert.Equal(expecterAllCharacters[index].Role, actualAllCharacters[index].Role);
                }
            }
        }
    }
}
