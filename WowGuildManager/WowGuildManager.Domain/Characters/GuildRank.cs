namespace WowGuildManager.Domain.Characters
{
    using System.ComponentModel.DataAnnotations;

    public class GuildRank
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
