namespace WowGuildManager.Domain.Raid
{
    using System.ComponentModel.DataAnnotations;
    public class RaidDestination
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [Range(10,40)]
        public int MaxPlayers { get; set; }

        [Required]
        public int TotalBosses { get; set; }

        [Required]
        public int KilledBosses { get; set; }
    }
}   
