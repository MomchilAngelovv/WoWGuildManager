using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Services.Characters
{
    //TOD0: Move this to dungeon service
    public interface ICharacterService
    {
        Character Create(CharacterCreateBindingModel inputModel);

        Character Delete(string characterId);

        IQueryable<T> GetClasses<T>();

        IQueryable<T> GetRoles<T>();

        IQueryable<T> GetCharactersByUserId<T>(string userId);

        T GetCharacterById<T>(string characterId);

        IQueryable<T> GetAll<T>();
    
        IQueryable<T> GetCharactersForDungeonByDungeonId<T>(string dungeonId);

        string GetClassIdByName(string className);

        string GetRoleIdByName(string roleName);

        string GetRankIdByName(string rankName);
    }
}
