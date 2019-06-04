using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Raid
{
    [Table("RaidCharacters")]
    public class RaidCharacters
    {
        public string RaidnId { get; set; }
        public Raid Dungeon { get; set; }


        public string CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
