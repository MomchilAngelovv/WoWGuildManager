using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Dungeon Create(DungeonCreateBindingModel inputModel)
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

            this.context.Dungeons.Add(dungeon);
            this.context.SaveChanges();

            return dungeon;
        }

        public IQueryable<T> GetAll<T>()
        {
            var dungeons = this.context.Dungeons
                .Include(dungeon => dungeon.Leader)
                .Include(dungeon => dungeon.RegisteredCharacters)
                .Select(dungeon => this.mapper.Map<T>(dungeon));

            return dungeons;

        }

        public IQueryable<T> GetDestinations<T>()
        {
            var dungeonDestinations = this.context.DungeonDestinations
                .Select(dd => mapper.Map<T>(dd));

            return dungeonDestinations;
        }

        public void RegisterCharacter(string characterId, string dungeonId)
        {
            var dungeonCharacter = new DungeonCharacter
            {
                CharacterId = characterId,
                DungeonId = dungeonId
            };

            this.context.DungeonCharacter.Add(dungeonCharacter);
            this.context.SaveChanges();
        }

        public T GetDestinationIdByDestinationName<T>(string destinationName)
        {
            var destinationId = mapper.Map<T>(this.context.DungeonDestinations
                .FirstOrDefault(dd => dd.Name == destinationName));

            return destinationId;
        }
    }
}
