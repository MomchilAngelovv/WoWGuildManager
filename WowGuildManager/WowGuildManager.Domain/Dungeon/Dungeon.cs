using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Dungeon
{
    [Table("Dungeons")]
    public class Dungeon
    {
        private const int DungeonMaxPlayers = 5;

        public Dungeon()
        {
            this.RegisteredCharacters = new HashSet<DungeonCharacters>();
        }

        [Key]
        public string Id { get; set; }

        public int MaxPlayers { get; set; } = DungeonMaxPlayers;

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public Character DungeonLeader { get; set; }

        public ICollection<DungeonCharacters> RegisteredCharacters { get; set; }
    }
}
