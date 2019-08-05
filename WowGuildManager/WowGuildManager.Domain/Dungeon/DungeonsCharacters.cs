namespace WowGuildManager.Domain.Dungeon
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Character;

    public class DungeonCharacter
    {
        [Key]
        public string Id { get; set; }
       
        public string DungeonId { get; set; }
        public virtual Dungeon Dungeon { get; set; }

        public string CharacterId { get; set; }
        public virtual Character Character { get; set; }
    }
}
