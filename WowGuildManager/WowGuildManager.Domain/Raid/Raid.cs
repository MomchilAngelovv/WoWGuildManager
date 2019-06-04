using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Raid
{

    [Table("Raids")]
    public class Raid
    {
        public Raid()
        {
            this.RegisteredCharacters = new HashSet<RaidCharacters>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public RaidPlace Place { get; set; }

        [Range(10, 40)]
        public int MaxPlayers { get; set; }

        [Range(10, 40)]
        public int RegisteredPlayers => this.RegisteredCharacters.Count;

        [Required]
        public string LeaderId { get; set; }
        public Character RaidLeader { get; set; }

        public ICollection<RaidCharacters> RegisteredCharacters { get; set; }
    }
}
