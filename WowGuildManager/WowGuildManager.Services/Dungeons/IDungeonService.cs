using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using System.Linq;
using WowGuildManager.Models.ViewModels.Dungeons;
using System.Threading.Tasks;

namespace WowGuildManager.Services.Dungeons
{
    public interface IDungeonService
    {
        Task<Dungeon> CreateAsync(DungeonCreateBindingModel inputModel);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetDungeonsForToday<T>();

        IEnumerable<T> GetDestinations<T>();

        Task<DungeonCharacter> RegisterCharacterAsync(string characterId, string dungeonId);

        IEnumerable<T> GetRegisteredCharactersByDungeonId<T>(string dungeonId);

        T GetDestinationIdByDestinationName<T>(string destinationName);
    }
}
