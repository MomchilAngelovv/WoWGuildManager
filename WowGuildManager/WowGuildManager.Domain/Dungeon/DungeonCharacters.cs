using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Dungeon
{
    [Table("DungeonCharacters")]
    public class DungeonCharacters
    {
        public string DungeonId { get; set; }
        public Dungeon Dungeon { get; set; }


        public string CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
