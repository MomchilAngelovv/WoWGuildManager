namespace WowGuildManager.Services.Guilds
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Services.Raids;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Common.GlobalConstants;

    public class GuildService : IGuildService
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly WowGuildManagerDbContext context;
        private readonly IRaidService raidService;
        private readonly ICharacterService characterService;
        private readonly IMapper mapper;

        public GuildService(
            UserManager<WowGuildManagerUser> userManager,
            WowGuildManagerDbContext context,
            IRaidService raidService,
            ICharacterService characterService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.context = context;
            this.raidService = raidService;
            this.characterService = characterService;
            this.mapper = mapper;
        }

        public async Task SetGuildMasterAsync(string userId)
        {
            var nextGuildMasterUser = this.context.Users.Find(userId);
            var previuosGuildMasterUser = this.context.Users.SingleOrDefault(u => u.IsGuildMaster == true);

            if (previuosGuildMasterUser != null)
            {
                await this.userManager.RemoveFromRoleAsync(previuosGuildMasterUser, WowGuildManagerUserConstants.GuildMaster);
                previuosGuildMasterUser.IsGuildMaster = false;
            }

            await this.userManager.AddToRoleAsync(nextGuildMasterUser, WowGuildManagerUserConstants.GuildMaster);
            nextGuildMasterUser.IsGuildMaster = true;
            await this.context.SaveChangesAsync();
        }
        public async Task SetOrUnsetRaidLeaderAsync(string userId)
        {
            var user = this.context.Users.Find(userId);

            if (user.IsRaidLeader)
            {
                await this.userManager.RemoveFromRoleAsync(user, WowGuildManagerUserConstants.RaidLeader);
                user.IsRaidLeader = false;
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, WowGuildManagerUserConstants.RaidLeader);
                user.IsRaidLeader = true;
            }

            await this.context.SaveChangesAsync();
        }
        public async Task PromoteRankAsync(string characterId)
        {
            var character = this.context.Characters.Find(characterId);

            if (character.Rank.Name != GuildRanksConstants.GuildMaster)
            {
                switch (character.Rank.Name)
                {
                    case GuildRanksConstants.Member:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Alt);
                        break;
                    case GuildRanksConstants.Alt:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.PvP);
                        break;
                    case GuildRanksConstants.PvP:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Veteran);
                        break;
                    case GuildRanksConstants.Veteran:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Raider);
                        break;
                    case GuildRanksConstants.Raider:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Officer);
                        break;
                    case GuildRanksConstants.Officer:
                        if (this.HasGuildMaster() == false)
                        {
                            character.RankId = this.characterService.GetRankId(GuildRanksConstants.GuildMaster);
                        }
                        else
                        {
                            throw new InvalidOperationException(ErrorConstants.AlreadyHasGuildMasterErrorMessage);
                        }
                        break;
                }

                this.context.Update(character);
                await this.context.SaveChangesAsync();
            }
        }
        public async Task DemoteRankAsync(string characterId)
        {
            var character = this.context.Characters.Find(characterId);

            if (character.Rank.Name != GuildRanksConstants.Member)
            {
                switch (character.Rank.Name)
                {
                    case GuildRanksConstants.GuildMaster:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Officer);
                        break;
                    case GuildRanksConstants.Officer:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Raider);
                        break;
                    case GuildRanksConstants.Raider:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Veteran);
                        break;
                    case GuildRanksConstants.Veteran:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.PvP);
                        break;
                    case GuildRanksConstants.PvP:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Alt);
                        break;
                    case GuildRanksConstants.Alt:
                        character.RankId = this.characterService.GetRankId(GuildRanksConstants.Member);
                        break;
                }

                this.context.Update(character);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task AddProgressToRaidAsync(string raidName)
        {
            var destination = this.raidService.GetDestination<RaidDestination>(raidName);

            if (destination.KilledBosses < destination.TotalBosses)
            {
                destination.KilledBosses += 1;
            }

            this.context.Update(destination);
            await this.context.SaveChangesAsync();
        }
        public async Task RemoveProgressToRaidAsync(string raidName)
        {
            var destination = this.raidService.GetDestination<RaidDestination>(raidName);

            if (destination.KilledBosses > 0)
            {
                destination.KilledBosses -= 1;
            }

            this.context.Update(destination);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> GetTotalRegisteredUsers<T>()
        {
            var users = this.context.Users
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return users;
        }

        public int GetTotalRegisteredUsersCount()
        {
            var usersCount = this.context.Users.Count();
            return usersCount;
        }
        public int GetTotalRegisteredCharactersCount()
        {
            var charactersCount = this.context.Characters.Count();
            return charactersCount;
        }

        private bool HasGuildMaster()
        {
            if (this.context.Characters.Any(ch => ch.Rank.Name == GuildRanksConstants.GuildMaster))
            {
                throw new InvalidOperationException(ErrorConstants.AlreadyHasGuildMasterErrorMessage);
            }

            return false;
        }
    }
}
