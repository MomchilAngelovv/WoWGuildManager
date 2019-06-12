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
        private const string DruidImage = "/images/classes/druidImg.jpg";
        private const string HunterImage = "/images/classes/hunterImg.jpg";
        private const string MageImage = "/images/classes/mageImg.jpg";
        private const string PaladinImage = "/images/classes/paladinImg.jpg";
        private const string PriestImage = "/images/classes/priestImg.jpg";
        private const string RogueImage = "/images/classes/rogueImg.jpg";
        private const string ShamanImage = "/images/classes/shamanImg.jpg";
        private const string WarlockImage = "/images/classes/warlockImg.jpg";
        private const string WarriorImage = "/images/classes/warriorImg.jpg";

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

            this.SetNewCharacterImage(character);

            this.context.Characters.Add(character);
            this.context.SaveChanges();

            return character;
        }

        public IEnumerable<Character> GetCharactersByUser(WowGuildManagerUser user)
        {
            return this.context.Characters
                .Where(character => character.User == user);
        }

        public IEnumerable<ClassType> GetClasses()
        {
            return Enum.GetValues(typeof(ClassType)).Cast<ClassType>().ToList();
        }

        public IEnumerable<CharacterRole> GetRoles()
        {
            return Enum.GetValues(typeof(CharacterRole)).Cast<CharacterRole>().ToList();
        }

        public Character GetCharacterById(string id)
        {
            return this.context.Characters
                .FirstOrDefault(c => c.Id == id);
        }

        private void SetNewCharacterImage(Character character)
        {
            switch (character.Class)
            {
                case ClassType.Druid:
                    character.Image = DruidImage;
                    break;
                case ClassType.Hunter:
                    character.Image = HunterImage;
                    break;
                case ClassType.Mage:
                    character.Image = MageImage;
                    break;
                case ClassType.Paladin:
                    character.Image = PaladinImage;
                    break;
                case ClassType.Priest:
                    character.Image = PriestImage;
                    break;
                case ClassType.Rogue:
                    character.Image = RogueImage;
                    break;
                case ClassType.Shaman:
                    character.Image = ShamanImage;
                    break;
                case ClassType.Warlock:
                    character.Image = WarlockImage;
                    break;
                case ClassType.Warrior:
                    character.Image = WarriorImage;
                    break;
            }
        }

        public IEnumerable<Character> GetAll()
        {
            return this.context.Characters.ToList();
        }
    }
}
