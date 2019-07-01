using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Models.ViewModels.Events
{
    //TODO FIX INTERFACES EVEYWHRE WHEN END!!!!!!!!!! IMPORTANT
    public class EventsIndexViewModel
    {
        public bool IsUserRaidLeaderOrGuildMaster { get; set; }

        public IEnumerable<RaidViewModel> Raids { get; set; }

        public IEnumerable<DungeonViewModel> Dungeons { get; set; }
    }
}
