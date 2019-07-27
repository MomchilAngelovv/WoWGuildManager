namespace WowGuildManager.Models.BindingModels.Characters
{
    using System.ComponentModel.DataAnnotations;

    public class CharacterEditBindingModel
    {
        [Required]
        public string CharacterId { get; set; }

        [Range(1, 60)]
        public int Level { get; set; }  

        [Required]
        public string Role { get; set; }
    }
}
