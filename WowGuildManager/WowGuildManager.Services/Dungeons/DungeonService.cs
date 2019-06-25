using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Services.Characters;

namespace WowGuildManager.Services.Dungeons
{
    public class DungeonService : IDungeonService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public DungeonService(WowGuildManagerDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Dungeon> CreateAsync(DungeonCreateBindingModel inputModel)
        {
            var dungeon = new Dungeon
            {
                EventDateTime = inputModel.DateTime,
                Description = inputModel.Description,
                Destination = this.GetDestinationIdByDestinationName<DungeonDestination>(inputModel.Destination),
                LeaderId = inputModel.LeaderId
            };

            dungeon.RegisteredCharacters.Add(new DungeonCharacter
            {
                CharacterId = dungeon.LeaderId,
                DungeonId = dungeon.Id
            });

            await this.context.Dungeons.AddAsync(dungeon);
            await this.context.SaveChangesAsync();

            return dungeon;
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            var dungeons = this.context.Dungeons
                .Where(r => r.EventDateTime.Day >= DateTime.Now.Day)
                .Include(dungeon => dungeon.Leader)
                .Include(dungeon => dungeon.RegisteredCharacters)
                .Include(dungeon => dungeon.Destination)
                .Select(dungeon => this.mapper.Map<T>(dungeon))
                .AsEnumerable();

            return dungeons;
        }

        public IEnumerable<T> GetDestinations<T>()
        {
            var dungeonDestinations = this.context.DungeonDestinations
                .Select(dd => mapper.Map<T>(dd))
                .AsEnumerable();

            return dungeonDestinations;
        }

        public async Task<DungeonCharacter> RegisterCharacter(string characterId, string dungeonId)
        {
            var dungeonCharacter = new DungeonCharacter
            {
                CharacterId = characterId,
                DungeonId = dungeonId
            };

            await this.context.DungeonCharacter.AddAsync(dungeonCharacter);
            await this.context.SaveChangesAsync();

            return dungeonCharacter;
        }

        public T GetDestinationIdByDestinationName<T>(string destinationName)
        {
            var destinationId = mapper.Map<T>(this.context.DungeonDestinations
                .FirstOrDefault(dd => dd.Name == destinationName));

            return destinationId;
        }

        public IEnumerable<T> GetDungeonsForToday<T>()
        {
            var dungeonsForToday = this.context.Dungeons
                .Where(d => d.EventDateTime.Day == DateTime.Now.Day)
                .Include(d => d.Destination)
                .Select(d => mapper.Map<T>(d))
                .AsEnumerable();

            return dungeonsForToday;
        }

        public IEnumerable<T> GetRegisteredCharactersByDungeonId<T>(string dungeonId)
        {
            var characters = this.context.DungeonCharacter
               .Where(dc => dc.DungeonId == dungeonId)
               .Include(rc => rc.Character)
               .ThenInclude(ch => ch.Role)
               .Include(rc => rc.Character)
               .ThenInclude(ch => ch.Class)
               .AsEnumerable()
               .Select(dc => mapper.Map<T>(dc.Character));

            return characters;
        }
    }
}
