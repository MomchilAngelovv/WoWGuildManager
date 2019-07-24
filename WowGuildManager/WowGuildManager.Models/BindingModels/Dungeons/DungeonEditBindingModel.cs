namespace WowGuildManager.Models.BindingModels.Dungeons
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DungeonEditBindingModel
    {
        [Required]
        public string DungeonId { get; set; }

        public string Description { get; set; }

        public DateTime? EventDateTime { get; set; }
    }
}
