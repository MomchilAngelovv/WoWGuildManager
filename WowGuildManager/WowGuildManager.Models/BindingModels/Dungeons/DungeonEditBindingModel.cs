using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.BindingModels.Dungeons
{
    public class DungeonEditBindingModel
    {
        public string DungeonId { get; set; }

        public string Description { get; set; }

        public DateTime? EventDateTime { get; set; }
    }
}
