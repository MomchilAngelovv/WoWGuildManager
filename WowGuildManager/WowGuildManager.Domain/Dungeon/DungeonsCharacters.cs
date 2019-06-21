using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Dungeon
{
    public class DungeonCharacter
    {
        [Required]
        public string DungeonId { get; set; }
        public virtual Dungeon Dungeon { get; set; }

        [Required]
        public string CharacterId { get; set; }
        public virtual Character Character { get; set; }
    }
}
