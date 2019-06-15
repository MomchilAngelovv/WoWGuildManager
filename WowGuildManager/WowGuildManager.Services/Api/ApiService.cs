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

        public ApiService(WowGuildManagerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Character> GetAll(string userId)
        {
            return this.context.Characters
                .Where(c => c.WowGuildManagerUserId == userId);
        }

        public Character GetCharacterById(string characterId)
        {
            return this.context.Characters.FirstOrDefault(c => c.Id == characterId);
        }
    }
}
