using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Services.Characters
{
    public interface ICharacterService
    {
        Character Create(CharacterCreateViewModel inputModel, WowGuildManagerUser user);

        ICollection<ClassType> GetClasses();

        ICollection<CharacterRole> GetRoles();
    }
}
