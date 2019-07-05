namespace WowGuildManager.Models.ApiModels.Raids
{
    public class RaidDestinationProgressApiViewModel
    {
        public string Name { get; set; }

        public int TotalBosses { get; set; }

        public int KilledBosses { get; set; }

        public double CompletedPercent => (double)((double)this.KilledBosses / (double)this.TotalBosses) * 100;
    }
}
