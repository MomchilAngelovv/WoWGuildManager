//TODO: SOrt interface methods
namespace WowGuildManager.Services.Raids
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.BindingModels.Raids;

    public interface IRaidService
    {
        Task<Raid> CreateAsync(RaidCreateBindingModel model);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetRaidsForToday<T>();

        IEnumerable<T> GetDestinations<T>();

        IEnumerable<T> GetRegisteredCharactersByRaidId<T>(string raidId);

        Task<RaidCharacter> RegisterCharacterAsync(string characterId, string raidId);

        string GetDestinationIdByName(string destinationName);

        T GetRaid<T>(string raidId);

        Task KickPlayer(string characterId, string raidId);
    }
}
    