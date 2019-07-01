namespace WowGuildManager.Domain.Dungeon
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Characters;
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
