namespace WowGuildManager.Domain.Character
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterClass
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
