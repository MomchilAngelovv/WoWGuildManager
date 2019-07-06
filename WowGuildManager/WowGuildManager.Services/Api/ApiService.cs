//TODO: Makeerror consntats
namespace WowGuildManager.Services.Api
{
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using WowGuildManager.Data;
    using WowGuildManager.Common.GlobalConstants;

    public class ApiService : IApiService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public ApiService(WowGuildManagerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidUserErrorMessage);
            }

            var characters = this.context.Characters
                .Where(c => c.WowGuildManagerUserId == userId)
                .Include(ch => ch.Class)
                .Include(ch => ch.Role)
                .Include(ch => ch.GuildRank)
                .Include(c => c.Dungeons)
                .Include(c => c.Raids)
                .Select(c => mapper.Map<T>(c));

            return characters;
        }

        public T GetCharacterById<T>(string characterId)
        {
            var character = this.context.Characters
                .Include(ch => ch.Class)
                .Include(ch => ch.Role)
                .Include(ch => ch.GuildRank)
                .Include(c => c.Raids)
                .Include(c => c.Dungeons)
                .FirstOrDefault(c => c.Id == characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            return mapper.Map<T>(character);
        }

        public IEnumerable<T> GuildProgress<T>()
        {
            var raidDestinations = this.context.RaidDestinations
              .Select(rd => mapper.Map<T>(rd))
              .AsEnumerable();

            return raidDestinations;
        }

        public IEnumerable<T> Members<T>()
        {
            var members = this.context.Characters
                .Include(ch => ch.Class)
                .Include(ch => ch.Role)
                .Include(ch => ch.GuildRank)
                .Include(ch => ch.Dungeons)
                .Include(ch => ch.Raids)
                .ToList()
                .Select(m => mapper.Map<T>(m));

            return members;
        }
    }
}
