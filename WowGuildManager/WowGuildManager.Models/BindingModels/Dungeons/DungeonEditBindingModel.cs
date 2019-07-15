namespace WowGuildManager.Models.BindingModels.Dungeons
{
    using System;

    public class DungeonEditBindingModel
    {
        public string DungeonId { get; set; }

        public string Description { get; set; }

        public DateTime? EventDateTime { get; set; }
    }
}
