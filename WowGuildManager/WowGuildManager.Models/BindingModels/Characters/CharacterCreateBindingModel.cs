//TODO: FIx identity area bootstrap and interface
namespace WowGuildManager.Models.BindingModels.Characters
{
    using System.ComponentModel.DataAnnotations;
    using WowGuildManager.Common.GlobalConstants;

    public class CharacterCreateBindingModel
    {
        public string UserId { get; set; }

        [Required]
        [MinLength(CharacterConstants.MinAllowedNameLength)]
        [MaxLength(CharacterConstants.MaxAllowedNameLength)]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }

        [Range(CharacterConstants.MinAllowedLevel, CharacterConstants.MaxAllowedLevel)]
        public int Level { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
