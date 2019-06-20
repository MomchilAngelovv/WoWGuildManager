using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public CharacterService(
            WowGuildManagerDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Character Create(CharacterCreateInputModel inputModel)
        {
            if (this.context.Characters.Where(c => c.WowGuildManagerUserId == inputModel.UserId).Count() == 4)
            {
                return null;
            }

            var character = new Character
            {
                Class = inputModel.Class,
                Level = inputModel.Level,
                Name = inputModel.Name,
                Role = inputModel.Role,
                WowGuildManagerUserId = inputModel.UserId,
            };

            this.SetCharacterImage(character);

            this.context.Characters.Add(character);
            this.context.SaveChanges();

            return character;
        }

        public IQueryable<T> GetCharactersByUserId<T>(string userId)
        {
            var characters = this.context.Characters
                .Where(character => character.WowGuildManagerUserId == userId)
                .Include(ch => ch.Dungeons)
                .Select(ch => mapper.Map<T>(ch));

            return characters;
        }

        public IQueryable<CharacterClass> GetClasses()
        {
            return Enum.GetValues(typeof(CharacterClass)).Cast<CharacterClass>().AsQueryable();
        }

        public IQueryable<CharacterRole> GetRoles()
        {
            return Enum.GetValues(typeof(CharacterRole)).Cast<CharacterRole>().AsQueryable();
        }

        public T GetCharacterById<T>(string characterId)
        {
            var character = this.context.Characters
                .Find(characterId);

            return mapper.Map<T>(character);
        }


        public IQueryable<T> GetAll<T>()
        {
            var characters = this.context.Characters
                .Select(c => mapper.Map<T>(c));

            return characters;
        }

        public Character Delete(string characterId)
        {
            var character = this.context.Characters.Find(characterId);

            this.context.Characters.Remove(character);
            this.context.SaveChanges();

            return character;
        }

        public IQueryable<T> GetCharactersForDungeonByDungeonId<T>(string id)
        {
            var characters = this.context.DungeonCharacter
                .Where(dc => dc.DungeonId == id)
                .Select(dc => mapper.Map<T>(dc.Character));

            return characters;
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
    }
}
