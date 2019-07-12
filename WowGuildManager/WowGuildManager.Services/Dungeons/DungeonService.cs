﻿//TODO: 100% make validator by ids
namespace WowGuildManager.Services.Dungeons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Data;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Models.BindingModels.Dungeons;

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
                DestinationId = this.GetDestinationIdByName(inputModel.Destination),
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
                .Where(r => r.EventDateTime >= DateTime.Now.AddHours(TimeConstants.HourDifferenceForUpcomingEvents))
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

        public async Task<DungeonCharacter> RegisterCharacterAsync(string characterId, string dungeonId)
        {
            var character = this.context.Characters
                .FirstOrDefault(ch => ch.Id == characterId);

            if (character == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidCharacterErrorMessage);
            }

            var dungeon = this.context.Dungeons
                .FirstOrDefault(d => d.Id == dungeonId);

            if (dungeon == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDungeonErrorMessage);
            }

            var dungeonCharacter = new DungeonCharacter
            {
                CharacterId = character.Id,
                DungeonId = dungeon.Id
            };

            await this.context.DungeonCharacter.AddAsync(dungeonCharacter);
            await this.context.SaveChangesAsync();

            return dungeonCharacter;
        }

        public T GetDestinationByDestinationName<T>(string destinationName)
        {
            var destination = mapper.Map<T>(this.context.DungeonDestinations
                .FirstOrDefault(dd => dd.Name == destinationName));

            if (destination == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDestinationNameErrorMessage);
            }

            return destination;
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
            var dungeon = this.context.Dungeons
              .FirstOrDefault(d => d.Id == dungeonId);

            if (dungeon == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDungeonErrorMessage);
            }

            var characters = this.context.DungeonCharacter
               .Where(dc => dc.DungeonId == dungeonId)
               .Include(rc => rc.Character)
               .ThenInclude(ch => ch.Role)
               .Include(rc => rc.Character)
               .ThenInclude(ch => ch.Class)
               .Include(rc => rc.Character)
               .ThenInclude(ch => ch.GuildRank)
               .AsEnumerable()
               .Select(dc => mapper.Map<T>(dc.Character));

            return characters;
        }

        public string GetDestinationIdByName(string destinationName)
        {
            var destination = this.context.DungeonDestinations
               .FirstOrDefault(dd => dd.Name == destinationName);

            if (destination == null)
            {
                throw new ArgumentException(ErrorConstants.InvalidDestinationNameErrorMessage);
            }

            return destination.Id;
        }

        public T GetDungeon<T>(string dungeonId)
        {
            var dungeon = this.context.Dungeons
                .Include(d => d.Destination)
                .FirstOrDefault(d => d.Id == dungeonId);

            if (dungeon == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidDungeonErrorMessage);
            }

            return mapper.Map<T>(dungeon);
        }

        public async Task KickCharacter(string characterId, string dungeonId)
        {
            var dungeonCharacter = this.context.DungeonCharacter
                .FirstOrDefault(rc => rc.CharacterId == characterId && rc.DungeonId == dungeonId);

            if (dungeonCharacter == null)
            {
                throw new InvalidOperationException(ErrorConstants.InvalidRaidKickErrorMessage);
            }

            this.context.DungeonCharacter.Remove(dungeonCharacter);
            await this.context.SaveChangesAsync();
        }
    }
}
