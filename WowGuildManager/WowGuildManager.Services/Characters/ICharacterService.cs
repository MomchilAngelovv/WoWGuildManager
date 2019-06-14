﻿using System;
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
        Character Create(CharacterCreateInputModel inputModel, WowGuildManagerUser user);

        IEnumerable<CharacterClass> GetClasses();

        IEnumerable<CharacterRole> GetRoles();

        IEnumerable<Character> GetCharactersByUser(WowGuildManagerUser user);

        Character GetCharacterById(string id);

        IEnumerable<Character> GetAll();
    }
}
