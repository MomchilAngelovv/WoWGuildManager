using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Guild
{
    public class GuildProgressViewModel
    {
        public IEnumerable<RaidDestinationProgressViewModel> RaidDestinationProgresses { get; set; }
    }
}
