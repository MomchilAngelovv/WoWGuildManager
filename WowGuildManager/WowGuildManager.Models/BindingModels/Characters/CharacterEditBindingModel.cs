namespace WowGuildManager.Models.BindingModels.Characters
{
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Common.GlobalConstants;

    public class CharacterEditBindingModel
    {
        [Required]
        public string CharacterId { get; set; }

        [Range(CharacterConstants.MinAllowedLevel, CharacterConstants.MaxAllowedLevel)]
        public int Level { get; set; }  

        [Required]
        public string Role { get; set; }
    }
}
