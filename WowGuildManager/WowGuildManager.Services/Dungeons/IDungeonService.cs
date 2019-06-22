using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using System.Linq;
using WowGuildManager.Models.ViewModels.Dungeons;

namespace WowGuildManager.Services.Dungeons
{
    public interface IDungeonService
    {
        Dungeon Create(DungeonCreateBindingModel inputModel);

        IQueryable<T> GetAll<T>();

        IQueryable<T> GetDungeonsForToday<T>();

        IQueryable<T> GetDestinations<T>();

        void RegisterCharacter(string characterId, string dungeonId);

        T GetDestinationIdByDestinationName<T>(string destinationName);
    }
}
