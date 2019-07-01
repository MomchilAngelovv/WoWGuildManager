namespace WowGuildManager.Domain.Dungeon
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Common;
    public class DungeonDestination
    {
        //TODO: REMOVE ALL MAGIC STRING AND NUMEBRS
        public DungeonDestination()
        {
            this.MaxPlayers = DungeonConstants.MaxPlayers;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public int MaxPlayers { get; set; }
    }
}
