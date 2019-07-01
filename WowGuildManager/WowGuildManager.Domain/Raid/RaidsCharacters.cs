namespace WowGuildManager.Domain.Raid
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Characters;
    public class RaidCharacter
    {
        [Required]
        public string RaidId { get; set; }
        public virtual Raid Raid { get; set; }

        [Required]
        public string CharacterId { get; set; }
        public virtual Character Character { get; set; }
    }
}
