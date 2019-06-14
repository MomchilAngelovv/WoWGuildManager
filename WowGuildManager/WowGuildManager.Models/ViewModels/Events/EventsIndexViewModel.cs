using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Models.ViewModels.Events
{
    public class EventsIndexViewModel
    {
        public IEnumerable<DungeonViewModel> Dungeons { get; set; }

        public IEnumerable<RaidViewModel> Raids { get; set; }
    }
}
