//TODO: Make test for EVERY service method
//TODO: OrderBy clouses where nesesary
//TODO: Delete remainning comments when READY ! IMPORTANT !!!
namespace WowGuildManager.Services.Characters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Models.BindingModels.Characters;

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

        public async Task<Character> CreateAsync(CharacterCreateBindingModel model)
        {
            if (this.UserHasMaxRegiresteredCharacters(model.UserId))
            {
                throw new InvalidOperationException(ErrorConstants.MaximumRegisteredPlayers);
            }

            var character = new Character
            {
                Name = model.Name,
                Level = model.Level,
                UserId = model.UserId,
                IsActive = true,
                ClassId = this.GetClassId(model.Class),
                RoleId = this.GetRoleId(model.Role),
                RankId = this.GetRankId(GuildRanksConstants.Member)
            };

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();

            return character;
        }
        public async Task<Character> EditAsync(CharacterEditBindingModel editModel)
        {
            var character = await this.context.Characters.FindAsync(editModel.CharacterId);

            character.Level = editModel.Level;
            character.RoleId = this.GetRoleId(editModel.Role);

            this.context.Update(character);
            await this.context.SaveChangesAsync();

            return character;
        }
        public async Task<Character> DeleteAsync(string characterId)
        {
            var character = this.context.Characters
                .Find(characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            character.IsActive = false;
            this.context.Update(character);

            await this.context.SaveChangesAsync();

            return character;
        }

        public IEnumerable<T> GetAllCharacters<T>()
        {
            var characters = this.context.Characters
                .Where(c => c.IsActive)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return characters;
        }
        public IEnumerable<T> GetUserCharacters<T>(string userId)
        {
            var characters = this.context.Characters
                .Where(character => character.UserId == userId && character.IsActive)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return characters;
        }
        public T GetCharacter<T>(string characterId)
        {
            var character = this.context.Characters
                .Find(characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            return mapper.Map<T>(character);
        }

        public IEnumerable<T> GetClassList<T>()
        {
            var classes = this.context.CharacterClasses
               .ProjectTo<T>(mapper.ConfigurationProvider)
               .ToList();

            return classes;
        }
        public IEnumerable<T> GetRoleList<T>()
        {
            var classes = this.context.CharacterRoles
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return classes;
        }

        public string GetClassId(string className)
        {
            var classObject = this.context.CharacterClasses
                .FirstOrDefault(cc => cc.Name == className);

            if (classObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidClassTypeErrorMessage);
            }

            return classObject.Id;
        }
        public string GetRoleId(string roleName)
        {
            var roleObject = this.context.CharacterRoles
                 .FirstOrDefault(cr => cr.Name == roleName);

            if (roleObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRoleTypeErrorMessage);
            }

            return roleObject.Id;
        }
        public string GetRankId(string rankName)
        {
            var rankObject = this.context.CharacterRanks
                 .FirstOrDefault(cr => cr.Name == rankName);

            if (rankObject == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRankTypeErrorMessage);
            }

            return rankObject.Id;
        }

        public bool UserHasMaxRegiresteredCharacters(string userId)
        {
            if (this.context.Characters.Where(c => c.UserId == userId && c.IsActive == true).Count() == CharacterConstants.MaximumAllowedCharactersPerUser)
            {
                return true;
            }

            return false;
        }
    }
}
