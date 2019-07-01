﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;



namespace WowGuildManager.Services.Characters
{
    //TOD0: Move this to dungeon service
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
    }
}
