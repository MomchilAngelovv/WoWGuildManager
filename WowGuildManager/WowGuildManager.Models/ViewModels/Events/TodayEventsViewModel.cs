namespace WowGuildManager.Models.ViewModels.Events
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Dungeons;
    using WowGuildManager.Models.ViewModels.Raids;
    public class TodayEventsViewModel
    {
        public IEnumerable<RaidIdDestinationViewModel> Raids { get; set; }

        public IEnumerable<DungeonIdDestinationViewModel> Dungeons { get; set; }
    }
}
