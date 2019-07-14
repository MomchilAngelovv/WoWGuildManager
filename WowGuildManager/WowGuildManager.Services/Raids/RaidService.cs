//TODO: Chage images for heroes to be better scaling with square
namespace WowGuildManager.Services.Raids
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Data;
    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.BindingModels.Raids;

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
            var destinationId = this.GetDestinationIdByName(model.Destination);

            var raid = new Raid
            {
                EventDateTime = model.DateTime,
                Description = model.Description,
                DestinationId = destinationId,
                LeaderId = model.LeaderId,
            };

            raid.RegisteredCharacters.Add(new RaidCharacter
            {
                CharacterId = model.LeaderId,
                RaidId = raid.Id
            });

            await this.context.Raids.AddAsync(raid);
            await this.context.SaveChangesAsync();

            return raid;
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            var raids = this.context.Raids
               .Where(r => r.EventDateTime >= DateTime.Now.AddHours(TimeConstants.HourDifferenceForUpcomingEvents))
               .ToList()
               .Select(raid => mapper.Map<T>(raid));
              
            return raids;
        }

        public IEnumerable<T> GetRaidsForToday<T>()
        {
            var raidsForToday = this.context.Raids
                .Where(d => d.EventDateTime.Day == DateTime.Now.Day)
                .ToList()
                .Select(d => mapper.Map<T>(d));

            return raidsForToday;
        }

        public IEnumerable<T> GetDestinations<T>()
        {
            var raidDestinations = this.context.RaidDestinations
                .Select(rd => mapper.Map<T>(rd))
                .ToList();

            return raidDestinations;
        }

        public IEnumerable<T> GetRegisteredCharactersByRaidId<T>(string raidId)
        {
            var raid = this.context.Raids
                .Find(raidId);

            if (raid == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRaidErrorMessage);
            }

            var registeredCharacters = this.context.RaidCharacter
                .Where(rc => rc.RaidId == raidId)
                .ToList()
                .Select(rc => mapper.Map<T>(rc.Character));
           
            return registeredCharacters;
        }

        public async Task<RaidCharacter> RegisterCharacterAsync(string characterId, string raidId)
        {
            var character = this.context.Characters
               .Find(characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            var raid = this.context.Raids
                .Find(raidId);

            if (raid == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRaidErrorMessage);
            }

            var raidCharacter = new RaidCharacter
            {
                CharacterId = character.Id,
                RaidId = raid.Id
            };

            await this.context.RaidCharacter.AddAsync(raidCharacter);
            await this.context.SaveChangesAsync();

            return raidCharacter;
        }

        public string GetDestinationIdByName(string destinationName)
        {
            var destination = this.context.RaidDestinations
                .FirstOrDefault(rd => rd.Name == destinationName);

            if (destination == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidDestinationNameErrorMessage);
            }

            return destination.Id;
        }

        public T GetRaid<T>(string raidId)
        {
            var raid = this.context.Raids
                .Find(raidId);

            if (raid == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidRaidErrorMessage);
            }

            return mapper.Map<T>(raid);
        }

        public async Task KickPlayer(string characterId, string raidId)
        {
            var raidCharacter = this.context.RaidCharacter
                .FirstOrDefault(rc => rc.CharacterId == characterId && rc.RaidId == raidId);

            if (raidCharacter == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidRaidKickErrorMessage);
            }

            this.context.RaidCharacter.Remove(raidCharacter);
            await this.context.SaveChangesAsync();
        }
    }
}
