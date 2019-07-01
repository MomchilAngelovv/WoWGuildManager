using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.BindingModels.Characters;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Services.Dungeons;

namespace WowGuildManager.Services.Characters
{
    //TODO: Make test for EVERY service method
    //TODO: Make all services throw exceptions instead of returning null
    //TODO: OrderBy clouses where nesesary
    //TODO: Delete remainning comments when READY ! IMPORTANT !!!
    public class CharacterService : ICharacterService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public CharacterService(WowGuildManagerDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Character> CreateAsync(CharacterCreateBindingModel model)
        {
            if (this.context.Characters.Where(c => c.WowGuildManagerUserId == model.UserId).Count() == 4)
            {
                throw new InvalidOperationException("User cannot have more than 4 characters in the guild");
            }

            var character = new Character
            {
                ClassId = this.GetClassIdByName(model.Class),
                Level = model.Level,
                Name = model.Name,
                RoleId = this.GetRoleIdByName(model.Role),
                WowGuildManagerUserId = model.UserId,
                GuildRankId = this.GetRankIdByName(GuildRanksConstants.Member),
            };

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> Delete(string characterId)
        {
            var character = this.context.Characters.Find(characterId);

            this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();

            return character;
        }

        public IEnumerable<T> GetClasses<T>()
        {
            var classes = this.context.CharacterClasses
               .Select(cc => mapper.Map<T>(cc))
               .AsEnumerable();

            return classes;
        }

        public IEnumerable<T> GetRoles<T>()
        {
            var classes = this.context.CharacterRoles
               .Select(cc => mapper.Map<T>(cc))
               .AsEnumerable();

            return classes;
        }

        public IEnumerable<T> GetCharactersByUserId<T>(string userId)
        {
            var characters = this.context.Characters
                .Where(character => character.WowGuildManagerUserId == userId)
                .Include(ch => ch.Dungeons)
                .Include(ch => ch.Role)
                .Include(ch => ch.Class)
                .Include(ch => ch.GuildRank)
                .Select(ch => mapper.Map<T>(ch))
                .AsEnumerable();

            return characters;
        }

        public T GetCharacterById<T>(string characterId)
        {
            var character = this.context.Characters
                .Include(ch => ch.Class)
                .Include(ch => ch.Role)
                .Include(ch => ch.GuildRank)
                .FirstOrDefault(ch => ch.Id == characterId);

            return mapper.Map<T>(character);
        }

        public IEnumerable<T> GetAll<T>()
        {
            var characters = this.context.Characters
                .Include(ch => ch.Dungeons)
                .Include(ch => ch.Role)
                .Include(ch => ch.Class)
                .Include(ch => ch.GuildRank)
                .Select(c => mapper.Map<T>(c))
                .ToList();

            return characters;
        }

        public string GetClassIdByName(string className)
        {
            var classObject = this.context.CharacterClasses
                .FirstOrDefault(cc => cc.Name == className);

            if (classObject == null)
            {
                throw new ArgumentException("Invalid class type!");
            }

            return classObject.Id; 
        }

        public string GetRoleIdByName(string roleName)
        {
            var roleObject = this.context.CharacterRoles
                 .FirstOrDefault(cr => cr.Name == roleName);

            if (roleObject == null)
            {
                throw new ArgumentException("Invalid role type!");
            }

            return roleObject.Id;
        }

        public string GetRankIdByName(string rankName)
        {
            var rankObject = this.context.GuildRanks
                 .FirstOrDefault(cr => cr.Name == rankName);

            if (rankObject == null)
            {
                throw new ArgumentException("Invalid rank type!");
            }

            return rankObject.Id;
        }

        public async Task Update(CharacterEditBindingModel model)
        {
            var character = this.GetCharacterById<Character>(model.Id);

            character.Level = model.Level;
            character.RoleId = this.GetRoleIdByName(model.Role);

            this.context.Update(character);
            await this.context.SaveChangesAsync();
        }
    }
}
