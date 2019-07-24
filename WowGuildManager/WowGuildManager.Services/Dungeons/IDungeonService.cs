namespace WowGuildManager.Services.Dungeons
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Models.BindingModels.Dungeons;

    public interface IDungeonService
    {
        Task<Dungeon> CreateAsync(DungeonCreateBindingModel inputModel);
        Task<Dungeon> EditAsync(DungeonEditBindingModel input);

        Task<DungeonCharacter> RegisterCharacterAsync(string dungeonId, string characterId);
        Task<DungeonCharacter> KickCharacterAsync(string dungeonId, string characterId);

        IEnumerable<T> GetAllUpcoming<T>();
        IEnumerable<T> GetTodayDungeons<T>();
        T GetDungeon<T>(string dungeonId);

        IEnumerable<T> GetRegisteredCharacters<T>(string dungeonId);

        IEnumerable<T> GetDestinations<T>();
        T GetDestination<T>(string dungeonName);
        string GetDestinationId(string dungeonName);
    }
}
