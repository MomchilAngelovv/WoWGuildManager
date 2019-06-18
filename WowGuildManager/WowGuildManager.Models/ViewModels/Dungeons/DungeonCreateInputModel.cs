using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WowGuildManager.Domain.Dungeon;

namespace WowGuildManager.Models.ViewModels.Dungeons
{
    public class DungeonCreateInputModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public DungeonPlace Place { get; set; }

        public string Description { get; set; }

        [Required]
        public string DungeonLeaderId { get; set; }
    }
}
