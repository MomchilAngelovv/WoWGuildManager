namespace WowGuildManager.Models.BindingModels.Dungeons
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Common.GlobalConstants;

    public class DungeonCreateBindingModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Destination { get; set; }

        [MaxLength(CommonConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string LeaderId { get; set; }
    }
}
