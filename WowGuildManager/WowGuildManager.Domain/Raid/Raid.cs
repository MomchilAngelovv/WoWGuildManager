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
            this.RegisteredCharacters = new HashSet<RaidsCharacters>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public RaidPlace Place { get; set; }

        [Range(10, 40)]
        public int MaxPlayers { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Image { get; set; }

        public string Description { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public Character Leader { get; set; }

        public ICollection<RaidsCharacters> RegisteredCharacters { get; set; }
    }
}
