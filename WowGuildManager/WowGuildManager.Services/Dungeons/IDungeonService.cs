namespace WowGuildManager.Services.Dungeons
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Models.BindingModels.Dungeons;

    public interface IDungeonService
    {
        Task<Dungeon> CreateAsync(DungeonCreateBindingModel inputModel);

        IEnumerable<T> GetAllUpcoming<T>();

        IEnumerable<T> GetDungeonsForToday<T>();

        IEnumerable<T> GetDestinations<T>();

        Task<DungeonCharacter> RegisterCharacterAsync(string characterId, string dungeonId);

        IEnumerable<T> GetRegisteredCharactersByDungeonId<T>(string dungeonId);

        T GetDestinationByDestinationName<T>(string destinationName);

        string GetDestinationIdByName(string destinationName);
    }
}
