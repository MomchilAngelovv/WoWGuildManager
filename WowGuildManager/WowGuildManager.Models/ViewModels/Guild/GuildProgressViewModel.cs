namespace WowGuildManager.Models.ViewModels.Guild
{
    using System.Collections.Generic;
    public class GuildProgressViewModel
    {
        public IEnumerable<RaidDestinationProgressViewModel> RaidDestinationProgresses { get; set; }

        public bool IsGuildMaster { get; set; }
    }
}
