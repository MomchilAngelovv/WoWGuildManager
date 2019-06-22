using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Services.Raids
{
    //TODO: SOrt interface methods
    public interface IRaidService
    {
        Raid Create(RaidCreateBindingModel model);

        IQueryable<T> GetAll<T>();

        IQueryable<T> GetDestinations<T>();

        IQueryable<T> GetRegisteredCharactersByRaidId<T>(string raidId);

        void RegisterCharacter(string characterId, string raidId);

        string GetDestinationIdByName(string destinationName);
    }
}
