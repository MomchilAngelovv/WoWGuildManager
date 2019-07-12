//TOD0: Move this to dungeon service
namespace WowGuildManager.Services.Characters
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Models.BindingModels.Characters;
    public interface ICharacterService
    {
        Task<Character> CreateAsync(CharacterCreateBindingModel inputModel);

        Task<Character> Delete(string characterId);

        IEnumerable<T> GetClasses<T>();

        IEnumerable<T> GetRoles<T>();

        IEnumerable<T> GetCharactersByUserId<T>(string userId);

        T GetCharacterById<T>(string characterId);

        IEnumerable<T> GetAll<T>();

        string GetClassIdByName(string className);

        string GetRoleIdByName(string roleName);

        string GetRankIdByName(string rankName);

        Task Update(CharacterEditBindingModel model);

        bool UserHasMaxRegiresteredCharacters(string userId);
    }
}
