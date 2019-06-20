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
        Character Create(CharacterCreateInputModel inputModel, string userId);

        IQueryable<CharacterClass> GetClasses();

        IQueryable<CharacterRole> GetRoles();

        IQueryable<Character> GetCharactersByUser(WowGuildManagerUser user);

        Character GetCharacterById(string id);

        IQueryable<Character> GetAll();

        Character Delete(string id);

        IQueryable<Character> GetCharactersForDungeonByDungeonId(string id);
    }
}
