using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Raid
{
    [Table("RaidCharacter")]
    public class RaidCharacter
    {
        [Required]
        public string RaidId { get; set; }
        public Raid Raid { get; set; }

        [Required]
        public string CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
