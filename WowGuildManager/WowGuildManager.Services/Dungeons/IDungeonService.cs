using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using System.Linq;
using WowGuildManager.Models.ViewModels.Dungeons;

namespace WowGuildManager.Services.Dungeons
{
    public interface IDungeonService
    {
        IQueryable<T> GetAll<T>();

        IQueryable<DungeonPlace> GetPlaces();

        Dungeon Create(DungeonCreateInputModel inputModel);

        void RegisterCharacter(string characterId, string dungeonId);
    }
}
