﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Services.Characters;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Services.Guilds
{
    //TODO: Excratd private method to set new character guild rank DRY
    public class GuildService : IGuildService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IRaidService raidService;
        private readonly ICharacterService characterService;

        public GuildService(
            WowGuildManagerDbContext context, 
            IMapper mapper, 
            UserManager<WowGuildManagerUser> userManager,
            IRaidService raidService,
            ICharacterService characterService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.raidService = raidService;
            this.characterService = characterService;
        }

        public async Task AddProgressToRaid(string raidName)
        {
            var destinationId = this.raidService.GetDestinationIdByName(raidName);

            var destination = this.context.RaidDestinations.FirstOrDefault(rd => rd.Id == destinationId);

            if (destination.KilledBosses < destination.TotalBosses)
            {
                destination.KilledBosses += 1;
            }

            this.context.Update(destination);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> GetRegisteredUsers<T>()
        {
            var users = this.context.Users
                .ToList()
                .Select(u => mapper.Map<T>(u));

            return users;
        }

        public async Task RemoveProgressToRaid(string raidName)
        {
            var destinationId = this.raidService.GetDestinationIdByName(raidName);

            var destination = this.context.RaidDestinations.FirstOrDefault(rd => rd.Id == destinationId);
            
            if (destination.KilledBosses > 0)
            {
                destination.KilledBosses -= 1;
            }

            this.context.Update(destination);
            await this.context.SaveChangesAsync();
        }

        public async Task SetGuildMasterAsync(string userId)
        {
            var user = this.context.Users
                .SingleOrDefault(u => u.Id == userId);

            var previuosGuildMaster = this.context.Users
                .FirstOrDefault(u => u.IsGuildMaster == true);

            if (previuosGuildMaster != null)
            {
                await this.userManager.RemoveFromRoleAsync(previuosGuildMaster, WowGuildManagerUserConstants.GuildMaster);
                previuosGuildMaster.IsGuildMaster = false;
            }

            await this.userManager.AddToRoleAsync(user, WowGuildManagerUserConstants.GuildMaster);
            user.IsGuildMaster = true;
            await this.context.SaveChangesAsync();
        }

        public async Task PromoteRankAsync(string characterId)
        {
            var character = this.characterService.GetCharacterById<Character>(characterId);

            if (character.GuildRank.Name != GuildRanksConstants.GuildMaster)
            {
                switch (character.GuildRank.Name)
                {
                    case GuildRanksConstants.Member:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Alt);
                        break;
                    case GuildRanksConstants.Alt:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.PvP);
                        break;
                    case GuildRanksConstants.PvP:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Veteran);
                        break;
                    case GuildRanksConstants.Veteran:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Raider);
                        break;
                    case GuildRanksConstants.Raider:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Officer);
                        break;
                    case GuildRanksConstants.Officer:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.GuildMaster);
                        break;
                }

                this.context.Update(character);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DemoteRankAsync(string characterId)
        {
            var character = this.characterService.GetCharacterById<Character>(characterId);

            if (character.GuildRank.Name != GuildRanksConstants.Member)
            {
                switch (character.GuildRank.Name)
                {
                    case GuildRanksConstants.GuildMaster:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Officer);
                        break;
                    case GuildRanksConstants.Officer:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Raider);
                        break;
                    case GuildRanksConstants.Raider:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Veteran);
                        break;
                    case GuildRanksConstants.Veteran:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.PvP);
                        break;
                    case GuildRanksConstants.PvP:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Alt);
                        break;
                    case GuildRanksConstants.Alt:
                        character.GuildRankId = this.characterService.GetRankIdByName(GuildRanksConstants.Member);
                        break;
                }

                this.context.Update(character);
                await this.context.SaveChangesAsync();
            }
        }
    }
}