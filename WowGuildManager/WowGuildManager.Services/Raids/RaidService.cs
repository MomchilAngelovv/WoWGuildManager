//TODO: Chage images for heroes to be better scaling with square
//TODO: See all netcoreapp versions or netstandard
namespace WowGuildManager.Services.Raids
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Models.BindingModels.Raids;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Domain.Characters;

    public class RaidService : IRaidService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly ICharacterService characterService;
        private readonly IMapper mapper;

        public RaidService(
            WowGuildManagerDbContext context,
            ICharacterService characterService,
            IMapper mapper)
        {
            this.context = context;
            this.characterService = characterService;
            this.mapper = mapper;
        }

        public async Task<Raid> CreateAsync(RaidCreateBindingModel createModel)
        {
            var destinationId = this.GetDestinationId(createModel.Destination);

            var raid = new Raid
            {
                EventDateTime = createModel.DateTime,
                Description = createModel.Description,
                DestinationId = destinationId,
                LeaderId = createModel.LeaderId,
            };

            raid.RegisteredCharacters.Add(new RaidCharacter
            {
                CharacterId = createModel.LeaderId,
                RaidId = raid.Id
            });

            await this.context.Raids.AddAsync(raid);
            await this.context.SaveChangesAsync();

            return raid;
        }
        public async Task<Raid> EditAsync(RaidEditBindingModel editModel)
        {
            var raid = this.GetRaid<Raid>(editModel.RaidId);

            if (string.IsNullOrWhiteSpace(editModel.Description) == false)
            {
                raid.Description = editModel.Description;
            }

            if (editModel.EventDateTime != null)
            {
                raid.EventDateTime = editModel.EventDateTime.Value;
            }

            this.context.Update(raid);
            await this.context.SaveChangesAsync();

            return raid;
        }

        public async Task<RaidCharacter> RegisterCharacterAsync(string raidId, string characterId)
        {
            var raid = this.GetRaid<Raid>(raidId);
            var character = this.characterService.GetCharacter<Character>(characterId);

            var raidCharacter = new RaidCharacter
            {
                RaidId = raid.Id,
                CharacterId = character.Id
            };

            await this.context.RaidCharacter.AddAsync(raidCharacter);
            await this.context.SaveChangesAsync();

            return raidCharacter;
        }
        public async Task<RaidCharacter> KickPlayerAsync(string raidId, string characterId)
        {
            var raidCharacter = this.context.RaidCharacter
                .FirstOrDefault(raidChar => raidChar.RaidId == raidId && raidChar.CharacterId == characterId);

            if (raidCharacter == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidRaidKickErrorMessage);
            }

            this.context.RaidCharacter.Remove(raidCharacter);
            await this.context.SaveChangesAsync();

            return raidCharacter;
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            var upcomingRaids = this.context.Raids
               .Where(raid => raid.EventDateTime >= DateTime.Now.AddHours(TimeConstants.HourDifferenceForUpcomingEvents) && raid.RegisteredCharacters.Any(regChar => regChar.Character.IsActive != false))
               .ProjectTo<T>(mapper.ConfigurationProvider)
               .ToList();
              
            return upcomingRaids;
        }
        public IEnumerable<T> GetTodayRaids<T>()
        {
            var todayRaids = this.context.Raids
                .Where(raid => raid.EventDateTime.Day == DateTime.Now.Day && raid.RegisteredCharacters.Any(regChar => regChar.Character.IsActive != false))
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return todayRaids;
        }
        public T GetRaid<T>(string raidId)
        {
            var raid = this.context.Raids
                .Find(raidId);

            if (raid == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidRaidErrorMessage);
            }

            var mappedRaid = mapper.Map<T>(raid);
            return mappedRaid;
        }

        public IEnumerable<T> GetRegisteredCharacters<T>(string raidId)
        {
            var raid = this.GetRaid<Raid>(raidId);

            var registeredCharacters = this.context.RaidCharacter
                .Where(regChar => regChar.RaidId == raidId && regChar.Character.IsActive)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();
           
            return registeredCharacters;
        }

        public IEnumerable<T> GetDestinations<T>()
        {
            var destinations = this.context.RaidDestinations
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return destinations;
        }
        public T GetDestination<T>(string raidName)
        {
            var destination = this.context.RaidDestinations
                .FirstOrDefault(raidDest => raidDest.Name == raidName);

            if (destination == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDestinationNameErrorMessage);
            }

            var mappedDestination = mapper.Map<T>(destination);
            return mappedDestination;
        }
        public string GetDestinationId(string raidName)
        {
            var destination = this.GetDestination<RaidDestination>(raidName);

            var destinationId = destination.Id;
            return destinationId;
        }
    }
}
