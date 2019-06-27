using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WowGuildManager.Services.Guilds
{
    public interface IGuildService
    {
        IEnumerable<T> GetRegisteredUsers<T>();

        Task SetGuildMasterAsync(string userId);

        Task AddProgressToRaid(string raidName);

        Task RemoveProgressToRaid(string raidName);
    }
}
