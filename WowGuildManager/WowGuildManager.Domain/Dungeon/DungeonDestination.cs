using System.ComponentModel.DataAnnotations;
using WowGuildManager.Common;

namespace WowGuildManager.Domain.Dungeon
{
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
