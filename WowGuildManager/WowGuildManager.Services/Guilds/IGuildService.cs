namespace WowGuildManager.Services.Guilds
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    public interface IGuildService
    {
        IEnumerable<T> GetRegisteredUsers<T>();

        Task AddProgressToRaid(string raidName);

        Task RemoveProgressToRaid(string raidName);

        Task PromoteRankAsync(string characterId);

        Task DemoteRankAsync(string characterId);
    }
}
