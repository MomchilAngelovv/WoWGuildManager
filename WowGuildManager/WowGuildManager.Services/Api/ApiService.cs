namespace WowGuildManager.Services.Api
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Common.GlobalConstants;

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

        public IEnumerable<T> GetAllMembers<T>()
        {
            var members = this.context.Characters
                .ToList()
                .Select(m => mapper.Map<T>(m));

            return members;
        }
        public IEnumerable<T> GetAllCharacters<T>(string userId)
        {
            var user = this.context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidUserErrorMessage);
            }

            var characters = this.context.Characters
                .Where(c => c.UserId == userId)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return characters;
        }
        public IEnumerable<T> GetAllImages<T>()
        {
            var images = this.context.GalleryImages
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return images;
        }
        public IEnumerable<T> GetAllExceptions<T>()
        {
            var exceptions = this.context.Errors
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return exceptions;
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

        public IEnumerable<T> GuildProgress<T>()
        {
            var raidDestinations = this.context.RaidDestinations
              .ProjectTo<T>(mapper.ConfigurationProvider)
              .ToList();

            return raidDestinations;
        }
    }
}
