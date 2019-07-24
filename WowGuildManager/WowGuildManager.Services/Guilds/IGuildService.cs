namespace WowGuildManager.Services.Guilds
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IGuildService
    {
        Task SetGuildMasterAsync(string userId);
        Task SetOrUnsetRaidLeaderAsync(string userId);
        Task PromoteRankAsync(string characterId);
        Task DemoteRankAsync(string characterId);

        Task AddProgressToRaidAsync(string raidName);
        Task RemoveProgressToRaidAsync(string raidName);

        IEnumerable<T> GetTotalRegisteredUsers<T>();

        int GetTotalRegisteredUsersCount();
        int GetTotalRegisteredCharactersCount();
    }
}
