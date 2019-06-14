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
            this.RegisteredCharacters = new HashSet<DungeonsCharacters>();
        }

        [Key]
        public string Id { get; set; }

        public int MaxPlayers { get; set; } = DungeonMaxPlayers;

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public DungeonPlace Place { get; set; }

        public string Description { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public Character Leader { get; set; }

        public ICollection<DungeonsCharacters> RegisteredCharacters { get; set; }
    }
}
