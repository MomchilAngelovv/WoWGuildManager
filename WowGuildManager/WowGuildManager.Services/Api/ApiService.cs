using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Services.Api
{
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

        public IQueryable<T> GetAll<T>(string userId)
        {
            var characters =  this.context.Characters
                .Where(c => c.WowGuildManagerUserId == userId)
                .Include(c => c.Dungeons)
                .Include(c => c.Raids)
                .Select(c => mapper.Map<T>(c));

            return characters;
        }

        public T GetCharacterById<T>(string characterId)
        {
            var character = this.context.Characters
                .Include(c => c.Dungeons)
                .Include(c => c.Raids)
                .FirstOrDefault(c => c.Id == characterId);

            return mapper.Map<T>(character);
        }
    }
}
