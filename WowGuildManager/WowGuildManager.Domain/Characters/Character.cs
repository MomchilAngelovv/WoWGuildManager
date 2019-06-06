using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Domain.Raid;

namespace WowGuildManager.Domain.Characters
{
    [Table("Characters")]
    public class Character
    {
      
        
        public Character()
        {
            this.Raids = new HashSet<RaidCharacters>();
            this.Dungeons = new HashSet<DungeonCharacters>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public ClassType Class { get; set; }

        [Range(1,60)]
        public int Level { get; set; }

        [Required]
        public CharacterRole Role { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string WowGuildManagerUserId { get; set; }
        public WowGuildManagerUser User { get; set; }

        public ICollection<RaidCharacters> Raids { get; set; }

        public ICollection<DungeonCharacters> Dungeons { get; set; }

    }
}
