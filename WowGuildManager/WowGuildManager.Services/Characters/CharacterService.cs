using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly WowGuildManagerDbContext context;

        public CharacterService(WowGuildManagerDbContext context)
        {
            this.context = context;
        }

        public Character Create(CharacterCreateViewModel inputModel, WowGuildManagerUser user)
        {
            var character = new Character
            {
                Class = inputModel.Class,
                Level = inputModel.Level,
                Name = inputModel.Name,
                Role = inputModel.Role,
                User = user,
            };

            this.context.Characters.Add(character);
            this.context.SaveChanges();

            return character;
        }

        public ICollection<ClassType> GetClasses()
        {
            return Enum.GetValues(typeof(ClassType)).Cast<ClassType>().ToList();
        }

        public ICollection<CharacterRole> GetRoles()
        {
            return Enum.GetValues(typeof(CharacterRole)).Cast<CharacterRole>().ToList();
        }
    }
}
