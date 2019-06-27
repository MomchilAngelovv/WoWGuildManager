using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Services.Guilds
{
    public class GuildService : IGuildService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly IRaidService raidService;

        public GuildService(
            WowGuildManagerDbContext context, 
            IMapper mapper, 
            UserManager<WowGuildManagerUser> userManager,
            IRaidService raidService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.raidService = raidService;
        }

        public async Task AddProgressToRaid(string raidName)
        {
            var destinationId = this.raidService.GetDestinationIdByName(raidName);

            var destination = this.context.RaidDestinations.FirstOrDefault(rd => rd.Id == destinationId);
            destination.KilledBosses += 1;

            if (destination.KilledBosses > destination.TotalBosses)
            {
                destination.KilledBosses = destination.TotalBosses;
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
            destination.KilledBosses -= 1;

            if (destination.KilledBosses < 0)
            {
                destination.KilledBosses = 0;
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
    }
}
