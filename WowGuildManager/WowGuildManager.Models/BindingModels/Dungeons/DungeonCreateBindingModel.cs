using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WowGuildManager.Domain.Dungeon;

namespace WowGuildManager.Models.ViewModels.Dungeons
{
    public class DungeonCreateBindingModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Destination { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string LeaderId { get; set; }
    }
}
