using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Services.Raids
{
    //TODO: SOrt interface methods
    public interface IRaidService
    {
        Task<Raid> CreateAsync(RaidCreateBindingModel model);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetRaidsForToday<T>();

        IEnumerable<T> GetDestinations<T>();

        IEnumerable<T> GetRegisteredCharactersByRaidId<T>(string raidId);

        Task<RaidCharacter> RegisterCharacterAsync(string characterId, string raidId);

        string GetDestinationIdByName(string destinationName);
    }
}
