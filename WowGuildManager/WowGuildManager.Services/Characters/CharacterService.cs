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

        public Character Create(CharacterCreateInputModel inputModel, string userId)
        {
            if (this.context.Characters.Where(c => c.WowGuildManagerUserId == userId).Count() == 4)
            {
                return null;
            }

            var character = new Character
            {
                Class = inputModel.Class,
                Level = inputModel.Level,
                Name = inputModel.Name,
                Role = inputModel.Role,
                WowGuildManagerUserId = userId,
            };

            this.SetCharacterImage(character);

            this.context.Characters.Add(character);
            this.context.SaveChanges();

            return character;
        }

        public IEnumerable<Character> GetCharactersByUser(WowGuildManagerUser user)
        {
            return this.context.Characters
                .Where(character => character.User == user);
        }

        public IEnumerable<CharacterClass> GetClasses()
        {
            return Enum.GetValues(typeof(CharacterClass)).Cast<CharacterClass>().ToList();
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

        private void SetCharacterImage(Character character)
        {
            switch (character.Class)
            {
                case CharacterClass.Druid:
                    character.Image = DruidImage;
                    break;
                case CharacterClass.Hunter:
                    character.Image = HunterImage;
                    break;
                case CharacterClass.Mage:
                    character.Image = MageImage;
                    break;
                case CharacterClass.Paladin:
                    character.Image = PaladinImage;
                    break;
                case CharacterClass.Priest:
                    character.Image = PriestImage;
                    break;
                case CharacterClass.Rogue:
                    character.Image = RogueImage;
                    break;
                case CharacterClass.Shaman:
                    character.Image = ShamanImage;
                    break;
                case CharacterClass.Warlock:
                    character.Image = WarlockImage;
                    break;
                case CharacterClass.Warrior:
                    character.Image = WarriorImage;
                    break;
            }
        }

        public IEnumerable<Character> GetAll()
        {
            return this.context.Characters.ToList();
        }

        public Character Delete(string id)
        {
            var character = this.GetCharacterById(id);

            this.context.Characters.Remove(character);
            this.context.SaveChanges();

            return character;
        }

        public IQueryable<Character> GetCharactersForDungeonByDungeonId(string id)
        {
            var characters = this.context.DungeonCharacter
                .Where(dc => dc.DungeonId == id)
                .Select(dc => dc.Character);

            return characters;
        }
    }
}
