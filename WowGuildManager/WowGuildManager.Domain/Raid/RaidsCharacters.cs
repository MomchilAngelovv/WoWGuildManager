namespace WowGuildManager.Domain.Raid
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Characters;

    public class RaidCharacter
    {
        [Key]
        public string Key { get; set; }

        public string RaidId { get; set; }
        public virtual Raid Raid { get; set; }

        public string CharacterId { get; set; }
        public virtual Character Character { get; set; }
    }
}
