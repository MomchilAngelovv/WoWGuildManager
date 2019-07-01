namespace WowGuildManager.Models.BindingModels.Dungeons
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class DungeonCreateBindingModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Destination { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string LeaderId { get; set; }
    }
}
