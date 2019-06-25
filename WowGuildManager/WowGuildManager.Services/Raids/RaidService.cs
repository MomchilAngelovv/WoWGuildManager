using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowGuildManager.Data;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;

//TODO: Chage images for heroes to be better scaling with square

namespace WowGuildManager.Services.Raids
{
    public class RaidService : IRaidService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public RaidService(WowGuildManagerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Raid> CreateAsync(RaidCreateBindingModel model)
        {
            var raid = new Raid
            {
                EventDateTime = model.DateTime,
                Description = model.Description,
                DestinationId = this.GetDestinationIdByName(model.Destination),
                LeaderId = model.LeaderId,
            };

            raid.RegisteredCharacters.Add(new RaidCharacter
            {
                CharacterId = model.LeaderId,
                Raid = raid
            });

            await this.context.Raids.AddAsync(raid);
            await this.context.SaveChangesAsync();

            return raid;
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            var raids = this.context.Raids
               .Where(r => r.EventDateTime.Day >= DateTime.Now.Day)
               .Include(raid => raid.Leader)
               .Include(raid => raid.RegisteredCharacters)
               .Include(raid => raid.Destination)
               .Select(raid => mapper.Map<T>(raid))
               .AsEnumerable();

            return raids;
        }

        public IEnumerable<T> GetRaidsForToday<T>()
        {
            var raidsForToday = this.context.Raids
                .Where(d => d.EventDateTime.Day == DateTime.Now.Day)
                .Include(d => d.Destination)
                .Select(d => mapper.Map<T>(d))
                .AsEnumerable();

            return raidsForToday;
        }

        public IEnumerable<T> GetDestinations<T>()
        {
            var raidDestinations = this.context.RaidDestinations
                .Select(rd => mapper.Map<T>(rd))
                .AsEnumerable();

            return raidDestinations;
        }

        public IEnumerable<T> GetRegisteredCharactersByRaidId<T>(string raidId)
        {
            var registeredCharacters = this.context.RaidCharacter
                .Where(rc => rc.RaidId == raidId)
                .Include(rc => rc.Character)
                .ThenInclude(ch => ch.Role)
                .Include(rc => rc.Character)
                .ThenInclude(ch => ch.Class)
                .AsEnumerable()
                .Select(rc => mapper.Map<T>(rc.Character));
           
            return registeredCharacters;
        }

        public async Task<RaidCharacter> RegisterCharacter(string characterId, string raidId)
        {
            var raidCharacter = new RaidCharacter
            {
                CharacterId = characterId,
                RaidId = raidId
            };

            await this.context.RaidCharacter.AddAsync(raidCharacter);
            await this.context.SaveChangesAsync();

            return raidCharacter;
        }

        public string GetDestinationIdByName(string destinationName)
        {
            var destinationId = this.context.RaidDestinations
                .FirstOrDefault(rd => rd.Name == destinationName)
                .Id;

            return destinationId;
        }
    }
}
