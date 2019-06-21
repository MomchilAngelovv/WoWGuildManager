using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Services.Dungeons;

namespace WowGuildManager.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public CharacterService(
            WowGuildManagerDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Character Create(CharacterCreateInputModel model)
        {
            if (this.context.Characters.Where(c => c.WowGuildManagerUserId == model.UserId).Count() == 4)
            {
                return null;
            }

            //TODO: MAGIC STRINGS FIX 100%
            var character = new Character
            {
                ClassId = this.GetClassIdByName(model.Class),
                Level = model.Level,
                Name = model.Name,
                RoleId = this.GetRoleIdByName(model.Role),
                WowGuildManagerUserId = model.UserId,
                GuildRankId = this.GetRankIdByName(GuildRanksConstants.Member),
            };

            this.context.Characters.Add(character);
            this.context.SaveChanges();

            return character;
        }

        public IQueryable<T> GetCharactersByUserId<T>(string userId)
        {
            var characters = this.context.Characters
                .Where(character => character.WowGuildManagerUserId == userId)
                .Include(ch => ch.Dungeons)
                .Include(ch => ch.Role)
                .Include(ch => ch.Class)
                .Include(ch => ch.GuildRank)
                .Select(ch => mapper.Map<T>(ch));

            return characters;
        }

        //TODO: Delete remainning comments when READY ! IMPORTANT !!!
        public IQueryable<T> GetClasses<T>()
        {
            var classes = this.context.CharacterClasses
               .Select(cc => mapper.Map<T>(cc));

            return classes;
        }

        public IQueryable<T> GetRoles<T>()
        {
            var classes = this.context.CharacterRoles
              .Select(cc => mapper.Map<T>(cc));

            return classes;
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
                .Include(ch => ch.Dungeons)
                .Include(ch => ch.Role)
                .Include(ch => ch.Class)
                .Include(ch => ch.GuildRank)
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

        public IQueryable<T> GetCharactersForDungeonByDungeonId<T>(string dungeonId)
        {
            var characters = this.context.DungeonCharacter
                .Where(dc => dc.DungeonId == dungeonId)
                .Select(dc => mapper.Map<T>(dc.Character));

            return characters;
        }

        public string GetClassIdByName(string className)
        {
            //TODO: FirstOrDefautl can return null and ID might explode
            var classId = this.context.CharacterClasses
                .FirstOrDefault(cc => cc.Name == className)
                .Id;

            return classId; 
        }

        public string GetRoleIdByName(string roleName)
        {
            var roleId = this.context.CharacterRoles
                 .FirstOrDefault(cr => cr.Name == roleName)
                 .Id;

            return roleId;
        }

        public string GetRankIdByName(string rankName)
        {
            var roleId = this.context.GuildRanks
                 .FirstOrDefault(cr => cr.Name == rankName)
                 .Id;

            return roleId;
        }
    }
}
