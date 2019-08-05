namespace WowGuildManager.Domain.Character
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterRole
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
