//TODO FIX INTERFACES EVEYWHRE WHEN END!!!!!!!!!! IMPORTANT
namespace WowGuildManager.Models.ViewModels.Events
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Dungeons;

    public class EventsIndexViewModel
    {
        public bool IsUserRaidLeaderOrGuildMaster { get; set; }

        public IEnumerable<RaidViewModel> Raids { get; set; }

        public IEnumerable<DungeonViewModel> Dungeons { get; set; }
    }
}
