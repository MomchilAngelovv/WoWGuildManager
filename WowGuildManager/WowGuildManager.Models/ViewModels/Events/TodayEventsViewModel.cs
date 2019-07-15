namespace WowGuildManager.Models.ViewModels.Events
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Dungeons;

    public class TodayEventsViewModel
    {
        public IEnumerable<RaidIdDestinationViewModel> Raids { get; set; }

        public IEnumerable<DungeonIdDestinationViewModel> Dungeons { get; set; }
    }
}
