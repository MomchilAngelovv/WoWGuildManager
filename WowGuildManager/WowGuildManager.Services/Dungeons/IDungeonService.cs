using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Dungeons;

namespace WowGuildManager.Services.Dungeons
{
    public interface IDungeonService
    {
        IEnumerable<Dungeon> GetAll();

        IEnumerable<DungeonPlace> GetPlaces();

        Dungeon Create(DungeonCreateInputModel inputModel);
    }
}
