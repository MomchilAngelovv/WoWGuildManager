using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Guild
{
    public class RaidDestinationProgressViewModel
    {
        public string Name { get; set; }

        public int TotalBosses { get; set; }

        public int KilledBosses { get; set; }

        public double CompletedPercent => (double)((double)this.KilledBosses / (double)this.TotalBosses) * 100;
    }
}
