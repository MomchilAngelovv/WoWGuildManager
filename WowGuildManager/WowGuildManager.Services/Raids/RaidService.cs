using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Data;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;

namespace WowGuildManager.Services.Raids
{
    //TODO: Chage images for heroes and Gnomeregan
    //TODO: Use lazyloading

    public class RaidService : IRaidService
    {
        private readonly WowGuildManagerDbContext context;
        private readonly IMapper mapper;

        public RaidService(WowGuildManagerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Raid Create(RaidCreateBindingModel model)
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

            this.context.Raids.Add(raid);
            this.context.SaveChanges();

            return raid;
        }

        public IQueryable<T> GetAll<T>()
        {
            var raids = this.context.Raids
               .Include(raid => raid.Leader)
               .Include(raid => raid.RegisteredCharacters)
               .Include(raid => raid.Destination)
               .Select(raid => mapper.Map<T>(raid));

            return raids;
        }

        public IQueryable<T> GetDestinations<T>()
        {
            var raidDestinations = this.context.RaidDestinations
                .Select(rd => mapper.Map<T>(rd));

            return raidDestinations;
        }

        public IQueryable<T> GetRegisteredCharactersByRaidId<T>(string raidId)
        {
            var registeredCharacters = this.context.RaidCharacter
                .Where(rc => rc.RaidId == raidId)
                .Include(rc => rc.Character)
                .Select(rc => mapper.Map<T>(rc.Character));

            return registeredCharacters;
        }

        public void RegisterCharacter(string characterId, string raidId)
        {
            var raidCharacter = new RaidCharacter
            {
                CharacterId = characterId,
                RaidId = raidId
            };

            this.context.RaidCharacter.Add(raidCharacter);
            this.context.SaveChanges();
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
