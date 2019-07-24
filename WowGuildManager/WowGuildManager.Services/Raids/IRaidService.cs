namespace WowGuildManager.Services.Raids
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.BindingModels.Raids;

    public interface IRaidService
    {
        Task<Raid> CreateAsync(RaidCreateBindingModel model);
        Task<Raid> EditAsync(RaidEditBindingModel input);

        Task<RaidCharacter> RegisterCharacterAsync(string raidId, string caracterId);
        Task<RaidCharacter> KickPlayerAsync(string characterId, string raidId);

        IEnumerable<T> GetAllUpcoming<T>();
        IEnumerable<T> GetTodayRaids<T>();
        T GetRaid<T>(string raidId);

        IEnumerable<T> GetRegisteredCharacters<T>(string raidId);

        IEnumerable<T> GetDestinations<T>();
        T GetDestination<T>(string raidName);
        string GetDestinationId(string raidName);
    }
}
    