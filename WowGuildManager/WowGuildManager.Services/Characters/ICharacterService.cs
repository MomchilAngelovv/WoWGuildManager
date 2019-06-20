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

        IQueryable<CharacterClass> GetClasses();

        IQueryable<CharacterRole> GetRoles();

        IQueryable<T> GetCharactersByUserId<T>(string userId);

        T GetCharacterById<T>(string characterId);

        IQueryable<T> GetAll<T>();

        IQueryable<T> GetCharactersForDungeonByDungeonId<T>(string id);
    }
}
