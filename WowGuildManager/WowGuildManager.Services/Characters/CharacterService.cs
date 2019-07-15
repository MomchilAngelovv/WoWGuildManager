//TODO: Make test for EVERY service method
//TODO: Make all services throw exceptions instead of returning null
//TODO: OrderBy clouses where nesesary
//TODO: Delete remainning comments when READY ! IMPORTANT !!!
//TODO: Implement Delete character
namespace WowGuildManager.Services.Characters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Models.BindingModels.Characters;

    public class CharacterService : ICharacterService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public CharacterService(WowGuildManagerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Character> CreateAsync(CharacterCreateBindingModel model)
        {
            if (this.UserHasMaxRegiresteredCharacters(model.UserId))
            {
                throw new InvalidOperationException(ErrorConstants.MaximumRegisteredPlayers);
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
            var character = this.context.Characters
                .Find(characterId);

            this.context.Characters.Remove(character);
            await this.context.SaveChangesAsync();

            return character;
        }

        public IEnumerable<T> GetClasses<T>()
        {
            var classes = this.context.CharacterClasses
               .Select(cc => mapper.Map<T>(cc))
               .ToList();

            return classes;
        }

        public IEnumerable<T> GetRoles<T>()
        {
            var classes = this.context.CharacterRoles
                .Select(cc => mapper.Map<T>(cc))
                .ToList();

            return classes;
        }

        public IEnumerable<T> GetCharactersByUserId<T>(string userId)
        {
            var characters = this.context.Characters
                .Where(character => character.WowGuildManagerUserId == userId)
                .ToList()
                .Select(ch => mapper.Map<T>(ch));

            return characters;
        }

        public T GetCharacterById<T>(string characterId)
        {
            var character = this.context.Characters
                .Find(characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            return mapper.Map<T>(character);
        }

        public IEnumerable<T> GetAll<T>()
        {
            var characters = this.context.Characters
                .ToList()
                .Select(c => mapper.Map<T>(c));

            return characters;
        }

        public string GetClassIdByName(string className)
        {
            var classObject = this.context.CharacterClasses
                .FirstOrDefault(cc => cc.Name == className);

            if (classObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidClassTypeErrorMessage);
            }

            return classObject.Id;
        }

        public string GetRoleIdByName(string roleName)
        {
            var roleObject = this.context.CharacterRoles
                 .FirstOrDefault(cr => cr.Name == roleName);

            if (roleObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRoleTypeErrorMessage);
            }

            return roleObject.Id;
        }

        public string GetRankIdByName(string rankName)
        {
            var rankObject = this.context.GuildRanks
                 .FirstOrDefault(cr => cr.Name == rankName);

            if (rankObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRankTypeErrorMessage);
            }

            return rankObject.Id;
        }

        public async Task Edit(CharacterEditBindingModel model)
        {
            var character = this.GetCharacterById<Character>(model.Id);

            character.Level = model.Level;
            character.RoleId = this.GetRoleIdByName(model.Role);

            this.context.Update(character);
            await this.context.SaveChangesAsync();
        }

        public bool UserHasMaxRegiresteredCharacters(string userId)
        {
            if (this.context.Characters.Where(c => c.WowGuildManagerUserId == userId).Count() == CharacterConstants.MaximumAllowedCharactersPerUser)
            {
                return true;
            }

            return false;
        }
    }
}
