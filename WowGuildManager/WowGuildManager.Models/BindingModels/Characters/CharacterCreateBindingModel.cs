//TODO: FIx identity area bootstrap and interface
//TODO: FIX CSS LAST
namespace WowGuildManager.Models.BindingModels.Characters
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterCreateBindingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }

        [Range(1, 60)]
        public int Level { get; set; }

        [Required]
        public string Role { get; set; }
        
        public string UserId { get; set; }
    }
}
