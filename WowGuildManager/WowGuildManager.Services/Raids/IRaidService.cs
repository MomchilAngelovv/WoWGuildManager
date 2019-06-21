using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Services.Raids
{
    public interface IRaidService
    {
        Raid Create(RaidCreateInputModel model);

        IQueryable<T> GetAll<T>();

        IQueryable<T> GetDestinations<T>();

        IQueryable<T> GetRegisteredCharactersByRaidId<T>(string raidId);

        void RegisterCharacter(string characterId, string raidId);

        //TODO: SOrt interface methods
        string GetDestinationIdByName(string destinationName);
    }
}
