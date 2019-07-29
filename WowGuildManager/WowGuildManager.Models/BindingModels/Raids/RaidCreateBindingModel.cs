namespace WowGuildManager.Models.BindingModels.Raids
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WowGuildManager.Common.GlobalConstants;

    public class RaidCreateBindingModel
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
