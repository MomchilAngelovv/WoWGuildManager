namespace WowGuildManager.Domain.Characters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Domain.Identity;

    public class Character  
    {
        public Character()
        {
            this.Raids = new HashSet<RaidCharacter>();
            this.Dungeons = new HashSet<DungeonCharacter>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(1, 60)]
        public int Level { get; set; }

        [Required]
        public int GuildPoints { get; set; }

        [Required]
        public string ClassId { get; set; }
        public virtual CharacterClass Class { get; set; }

        [Required]
        public string RoleId { get; set; }
        public virtual CharacterRole Role { get; set; }

        [Required]
        public string GuildRankId { get; set; }
        public virtual GuildRank GuildRank { get; set; }

        [Required]
        public string WowGuildManagerUserId { get; set; }
        public virtual WowGuildManagerUser User { get; set; }

        public virtual ICollection<RaidCharacter> Raids { get; set; }

        public virtual ICollection<DungeonCharacter> Dungeons { get; set; }
    }
}
