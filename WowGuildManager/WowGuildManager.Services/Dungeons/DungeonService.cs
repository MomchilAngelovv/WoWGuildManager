namespace WowGuildManager.Services.Dungeons
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Models.BindingModels.Dungeons;
    using WowGuildManager.Services.Characters;
    using WowGuildManager.Domain.Characters;

    public class DungeonService : IDungeonService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly ICharacterService characterService;
        private readonly IMapper mapper;

        public DungeonService(
            WowGuildManagerDbContext context,
            ICharacterService characterService,
            IMapper mapper)
        {
            this.context = context;
            this.characterService = characterService;
            this.mapper = mapper;
        }

        public async Task<Dungeon> CreateAsync(DungeonCreateBindingModel createModel)
        {
            var dungeon = new Dungeon
            {
                EventDateTime = createModel.DateTime,
                Description = createModel.Description,
                LeaderId = createModel.LeaderId,
                DestinationId = this.GetDestinationId(createModel.Destination)
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
        public async Task<Dungeon> EditAsync(DungeonEditBindingModel editModel)
        {
            var dungeon = this.GetDungeon<Dungeon>(editModel.DungeonId);

            if (string.IsNullOrWhiteSpace(editModel.Description) == false)
            {
                dungeon.Description = editModel.Description;
            }

            if (editModel.EventDateTime != null)
            {
                dungeon.EventDateTime = editModel.EventDateTime.Value;
            }

            this.context.Update(dungeon);
            await this.context.SaveChangesAsync();

            return dungeon;
        }

        public async Task<DungeonCharacter> RegisterCharacterAsync(string dungeonId, string characterId)
        {
            var dungeon = this.GetDungeon<Dungeon>(dungeonId);
            var character = this.characterService.GetCharacter<Character>(characterId);

            var dungeonCharacter = new DungeonCharacter
            {
                DungeonId = dungeon.Id,
                CharacterId = character.Id
            };

            await this.context.DungeonCharacter.AddAsync(dungeonCharacter);
            await this.context.SaveChangesAsync();

            return dungeonCharacter;
        }
        public async Task<DungeonCharacter> KickCharacterAsync(string dungeonId, string characterId)
        {
            var dungeonCharacter = this.context.DungeonCharacter
                .FirstOrDefault(rc => rc.DungeonId == dungeonId && rc.CharacterId == characterId);

            if (dungeonCharacter == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidCharacterKickErrorMessage);
            }

            this.context.DungeonCharacter.Remove(dungeonCharacter);
            await this.context.SaveChangesAsync();

            return dungeonCharacter;
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            var upcomingDungeons = this.context.Dungeons
                .Where(dung => dung.EventDateTime >= DateTime.Now.AddHours(TimeConstants.HourDifferenceForUpcomingEvents) && dung.RegisteredCharacters.Any(regChar => regChar.Character.IsActive != false))
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return upcomingDungeons;
        }
        public IEnumerable<T> GetTodayDungeons<T>()
        {
            var todayDungeons = this.context.Dungeons
                .Where(dung => dung.EventDateTime.Day == DateTime.Now.Day && dung.RegisteredCharacters.Any(regChar => regChar.Character.IsActive != false))
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return todayDungeons;
        }
        public T GetDungeon<T>(string dungeonId)
        {
            var dungeon = this.context.Dungeons
                .Find(dungeonId);

            if (dungeon == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidDungeonErrorMessage);
            }

            var mappedDungeon = mapper.Map<T>(dungeon);
            return mappedDungeon;
        }

        public IEnumerable<T> GetRegisteredCharacters<T>(string dungeonId)
        {
            var dungeon = this.GetDungeon<Dungeon>(dungeonId);

            var registeredCharacters = this.context.DungeonCharacter
               .Where(dc => dc.DungeonId == dungeonId && dc.Character.IsActive)
               .ProjectTo<T>(mapper.ConfigurationProvider)
               .ToList();

            return registeredCharacters;
        }

        public IEnumerable<T> GetDestinations<T>()
        {
            var destinations = this.context.DungeonDestinations
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return destinations;
        }
        public T GetDestination<T>(string dungeonName)
        {
            var destination = this.context.DungeonDestinations
                .FirstOrDefault(dest => dest.Name == dungeonName);

            if (destination == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDestinationNameErrorMessage);
            }

            var mappedDestination = mapper.Map<T>(destination);
            return mappedDestination;
        }
        public string GetDestinationId(string dungeonName)
        {
            var destination = this.GetDestination<DungeonDestination>(dungeonName);

            var destinationId = destination.Id;
            return destinationId;
        }
    }
}
