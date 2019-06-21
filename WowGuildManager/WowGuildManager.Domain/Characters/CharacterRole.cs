using System.ComponentModel.DataAnnotations;

namespace WowGuildManager.Domain.Characters
{
    public class CharacterRole
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
