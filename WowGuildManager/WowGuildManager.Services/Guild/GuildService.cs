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

namespace WowGuildManager.Services.Guilds
{
    public class GuildService : IGuildService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<WowGuildManagerUser> userManager;

        public GuildService(WowGuildManagerDbContext context, IMapper mapper, UserManager<WowGuildManagerUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IEnumerable<T> GetRegisteredUsers<T>()
        {
            var users = this.context.Users
                .ToList()
                .Select(u => mapper.Map<T>(u));

            return users;
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
