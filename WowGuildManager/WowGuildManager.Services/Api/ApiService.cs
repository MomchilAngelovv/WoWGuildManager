namespace WowGuildManager.Services.Api
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ApiModels.Raids;

    public class ApiService : IApiService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public ApiService(
            WowGuildManagerDbContext context, 
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<CharacterApiViewModel> GetAllMembers()
        {
            var members = this.context.Characters
                .ProjectTo<CharacterApiViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return members;
        }
        public IEnumerable<CharacterApiViewModel> GetAllCharacters(string userId)
        {
            var user = this.context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidUserErrorMessage);
            }

            var characters = this.context.Characters
                .Where(c => c.UserId == userId)
                .ProjectTo<CharacterApiViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return characters;
        }
        public IEnumerable<ImageApiViewModel> GetAllImages()
        {
            var images = this.context.GalleryImages
                .ProjectTo<ImageApiViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return images;
        }
        public IEnumerable<ExceptionApiViewModel> GetAllExceptions()
        {
            var exceptions = this.context.Errors
                .ProjectTo<ExceptionApiViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return exceptions;
        }

        public CharacterApiViewModel GetCharacter(string characterId)
        {
            var character = this.context.Characters
                .Find(characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            return mapper.Map<CharacterApiViewModel>(character);
        }

        public IEnumerable<RaidDestinationProgressApiViewModel> GuildProgress()
        {
            var raidDestinations = this.context.RaidDestinations
              .ProjectTo<RaidDestinationProgressApiViewModel>(mapper.ConfigurationProvider)
              .ToList();

            return raidDestinations;
        }
    }
}
