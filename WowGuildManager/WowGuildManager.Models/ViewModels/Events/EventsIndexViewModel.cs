using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Dungeons;

namespace WowGuildManager.Models.ViewModels.Events
{
    public class EventsIndexViewModel
    {
        public IEnumerable<DungeonViewModel> Dungeons { get; set; }
    }
}
