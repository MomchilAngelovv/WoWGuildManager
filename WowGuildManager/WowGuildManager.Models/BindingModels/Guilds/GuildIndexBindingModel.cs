namespace WowGuildManager.Models.BindingModels.Guilds
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Raids;
    using WowGuildManager.Models.ViewModels.Users;

    public class GuildMasterViewModel
    {
        public int RegisteredCharactersCount { get; set; }

        public IEnumerable<UserGuildMasterViewModel> Users { get; set; }

        public IEnumerable<RaidDestinationGuildMasterProgressViewModel> RaidDestinations { get; set; }
    }
}
