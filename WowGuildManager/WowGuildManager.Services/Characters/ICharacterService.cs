using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Services.Characters
{
    public interface ICharacterService
    {
        Character Create(CharacterCreateInputModel inputModel);

        Character Delete(string characterId);

        IQueryable<T> GetClasses<T>();

        IQueryable<T> GetRoles<T>();

        IQueryable<T> GetCharactersByUserId<T>(string userId);

        T GetCharacterById<T>(string characterId);

        IQueryable<T> GetAll<T>();

        //TODO: Refactor and be consistend with id or dungeon id
        //TOD0: Move this to dungeon service
        IQueryable<T> GetCharactersForDungeonByDungeonId<T>(string dungeonId);

        string GetClassIdByName(string className);

        string GetRoleIdByName(string roleName);
    }
}
