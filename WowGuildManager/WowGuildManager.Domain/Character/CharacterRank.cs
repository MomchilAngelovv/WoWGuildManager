namespace WowGuildManager.Domain.Character
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterRank
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
