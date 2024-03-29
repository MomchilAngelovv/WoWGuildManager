﻿namespace WowGuildManager.Services.Characters
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using WowGuildManager.Domain.Character;
    using WowGuildManager.Models.BindingModels.Characters;

    public interface ICharacterService
    {
        Task<Character> CreateAsync(CharacterCreateBindingModel createModel);
        Task<Character> EditAsync(CharacterEditBindingModel editModel, string userId);
        Task<Character> DeleteAsync(string characterId, string userId);

        IEnumerable<T> GetAllCharacters<T>();
        IEnumerable<T> GetUserCharacters<T>(string userId);
        T GetCharacter<T>(string characterId);

        IEnumerable<T> GetClassList<T>();
        IEnumerable<T> GetRoleList<T>();

        string GetClassId(string className);
        string GetRoleId(string roleName);
        string GetRankId(string rankName);

        bool UserHasMaxRegiresteredCharacters(string userId);
    }
}
