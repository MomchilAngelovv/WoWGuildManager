using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Models.ViewModels.Events
{
    public class TodayEventsViewModel
    {
        public IEnumerable<RaidIdDestinationViewModel> Raids { get; set; }

        public IEnumerable<DungeonIdDestinationViewModel> Dungeons { get; set; }
    }
}
