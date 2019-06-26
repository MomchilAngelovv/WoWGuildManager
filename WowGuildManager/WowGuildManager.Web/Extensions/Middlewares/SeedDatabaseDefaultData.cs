﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Common;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Domain.Raid;
using System.Reflection;

namespace WowGuildManager.Web.Extensions
{
    public class SeedDatabaseDefaultData
    {
        private readonly RequestDelegate _next;


        public SeedDatabaseDefaultData(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, WowGuildManagerDbContext context)
        {
            if (context.CharacterClasses.Any() == false)
            {
                await this.SeedCharacterClasses(context);
            }

            if (context.CharacterRoles.Any() == false)
            {
                await this.SeedCharacterRoles(context);
            }

            if (context.DungeonDestinations.Any() == false)
            {
                await this.SeedDungeonDestinations(context);
            }

            if (context.RaidDestinations.Any() == false)
            {
                await this.SeedRaidDestinations(context);
            }

            if (context.GuildRanks.Any() == false)
            {
                await this.SeedGuildRanks(context);
            }

            await _next(httpContext);
        }

        private async Task SeedGuildRanks(WowGuildManagerDbContext context)
        {
            var guildrankNames = new List<string>
            {
                GuildRanksConstants.GuildMaster,
                GuildRanksConstants.Officer,
                GuildRanksConstants.Veteran,
                GuildRanksConstants.Raider,
                GuildRanksConstants.PvP,
                GuildRanksConstants.Member,
                GuildRanksConstants.Alt,
            };

            var guildRanks = new List<GuildRank>();

            foreach (var guildRankName in guildrankNames)
            {
                var guildRank = new GuildRank
                {
                    Name = guildRankName
                };

                guildRanks.Add(guildRank);
            }

            await context.GuildRanks.AddRangeAsync(guildRanks);
            await context.SaveChangesAsync();
        }

        private async Task SeedRaidDestinations(WowGuildManagerDbContext context)
        {
            var raidNamesAndMaxPlayers = new Dictionary<string, int>
            {
                [RaidConstants.Ubrs] = 10,
                [RaidConstants.Zg] = 20,
                [RaidConstants.Aq20] = 20,
                [RaidConstants.Mc] = 40,
                [RaidConstants.Ony] = 40,
                [RaidConstants.Bwl] = 40,
                [RaidConstants.Aq40] = 40,
                [RaidConstants.Naxx] = 40,
            };

            //TODO:Consider full dungeon and raid names!!
            var raidDestinations = new List<RaidDestination>();

            foreach (var namdAndMaxPlayers in raidNamesAndMaxPlayers)
            {
                var raidDestination = new RaidDestination
                {
                    Name = namdAndMaxPlayers.Key,
                    MaxPlayers = namdAndMaxPlayers.Value,
                };

                switch (raidDestination.Name)
                {
                    case RaidConstants.Ubrs:
                        raidDestination.ImagePath = RaidConstants.UbrsImage;
                        raidDestination.TotalBosses = RaidConstants.UbrsTotalBosses;
                        break;
                    case RaidConstants.Zg:
                        raidDestination.ImagePath = RaidConstants.ZgImage;
                        raidDestination.TotalBosses = RaidConstants.ZgTotalBosses;
                        break;
                    case RaidConstants.Aq20:
                        raidDestination.ImagePath = RaidConstants.Aq20Image;
                        raidDestination.TotalBosses = RaidConstants.Aq20TotalBosses;
                        break;
                    case RaidConstants.Aq40:
                        raidDestination.ImagePath = RaidConstants.Aq40Image;
                        raidDestination.TotalBosses = RaidConstants.Aq40TotalBosses;
                        break;
                    case RaidConstants.Naxx:
                        raidDestination.ImagePath = RaidConstants.NaxxImage;
                        raidDestination.TotalBosses = RaidConstants.NaxxTotalBosses;
                        break;
                    case RaidConstants.Mc:
                        raidDestination.ImagePath = RaidConstants.McImage;
                        raidDestination.TotalBosses = RaidConstants.McTotalBosses;
                        break;
                    case RaidConstants.Ony:
                        raidDestination.ImagePath = RaidConstants.OnyImage;
                        raidDestination.TotalBosses = RaidConstants.OnyTotalBosses;
                        break;
                    case RaidConstants.Bwl:
                        raidDestination.ImagePath = RaidConstants.BwlImage;
                        raidDestination.TotalBosses = RaidConstants.BwlTotalBosses;
                        break;
                }

                raidDestinations.Add(raidDestination);
            }

            await context.RaidDestinations.AddRangeAsync(raidDestinations);
            await context.SaveChangesAsync();
        }

        private async Task SeedDungeonDestinations(WowGuildManagerDbContext context)
        {
            var dungeonNamesAnddungeonImagePaths = new Dictionary<string, string>
            {
                [DungeonConstants.Rfc] = DungeonConstants.RfcImage,
                [DungeonConstants.Wc] = DungeonConstants.WcImage,
                [DungeonConstants.Dm] = DungeonConstants.DmImage,
                [DungeonConstants.Sfk] = DungeonConstants.SfkImage,
                [DungeonConstants.Bfd] = DungeonConstants.BfdImage,
                [DungeonConstants.Stocks] = DungeonConstants.StocksImage,
                [DungeonConstants.Gnome] = DungeonConstants.GnomeImage,
                [DungeonConstants.Sm] = DungeonConstants.SmImage,
                [DungeonConstants.Rfk] = DungeonConstants.RfkImage,
                [DungeonConstants.Mara] = DungeonConstants.MaraImage,
                [DungeonConstants.Rfd] = DungeonConstants.RfdImage,
                [DungeonConstants.Diremaul] = DungeonConstants.DiremaulImage,
                [DungeonConstants.Scholo] = DungeonConstants.ScholoImage,
                [DungeonConstants.Ulda] = DungeonConstants.UldaImage,
                [DungeonConstants.Strat] = DungeonConstants.StratImage,
                [DungeonConstants.Zf] = DungeonConstants.ZfImage,
                [DungeonConstants.Brd] = DungeonConstants.BrdImage,
                [DungeonConstants.St] = DungeonConstants.StImage,
                [DungeonConstants.Lbrs] = DungeonConstants.LbrsImage,
            };

            var dungeonDestinations = new List<DungeonDestination>();

            foreach (var nameAndImagePath in dungeonNamesAnddungeonImagePaths)
            {
                var dungeonDestination = new DungeonDestination
                {
                    Name = nameAndImagePath.Key,
                    ImagePath = nameAndImagePath.Value
                };

                dungeonDestinations.Add(dungeonDestination);
            }

            await context.DungeonDestinations.AddRangeAsync(dungeonDestinations);
            await context.SaveChangesAsync();
        }

        private async Task SeedCharacterRoles(WowGuildManagerDbContext context)
        {
            var roleNames = new List<string>
            {
                CharacterRoleConstants.Tank,
                CharacterRoleConstants.Healer,
                CharacterRoleConstants.Damage
            };

            var characterRoles = new List<CharacterRole>();

            foreach (var roleName in roleNames)
            {
                var characterRole = new CharacterRole
                {
                    Name = roleName
                };

                characterRoles.Add(characterRole);
            }

            await context.CharacterRoles.AddRangeAsync(characterRoles);
            await context.SaveChangesAsync();
        }

        private async Task SeedCharacterClasses(WowGuildManagerDbContext context)
        {
            var classNamesAndImagePaths = new Dictionary<string, string>
            {
                [CharacterConstants.Druid] = CharacterConstants.DruidImage,
                [CharacterConstants.Hunter] = CharacterConstants.HunterImage,
                [CharacterConstants.Mage] = CharacterConstants.MageImage,
                [CharacterConstants.Paladin] = CharacterConstants.PaladinImage,
                [CharacterConstants.Priest] = CharacterConstants.PriestImage,
                [CharacterConstants.Rogue] = CharacterConstants.RogueImage,
                [CharacterConstants.Shaman] = CharacterConstants.ShamanImage,
                [CharacterConstants.Warlock] = CharacterConstants.WarlockImage,
                [CharacterConstants.Warrior] = CharacterConstants.WarriorImage,
            };

            var characterClasses = new List<CharacterClass>();

            foreach (var classNameAndImagePath in classNamesAndImagePaths)
            {
                var characterClass = new CharacterClass
                {
                    Name = classNameAndImagePath.Key,
                    ImagePath = classNameAndImagePath.Value
                };

                characterClasses.Add(characterClass);
            }

            await context.CharacterClasses.AddRangeAsync(characterClasses);
            await context.SaveChangesAsync();
        }
    }
}
