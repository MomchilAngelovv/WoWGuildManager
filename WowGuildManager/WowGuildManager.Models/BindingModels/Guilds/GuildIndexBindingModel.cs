using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Characters;
using WowGuildManager.Models.ViewModels.Guild;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Models.ViewModels.Users;

namespace WowGuildManager.Models.BindingModels.Guilds
{
    public class GuildMasterViewModel
    {
        public int RegisteredCharactersCount { get; set; }

        public IEnumerable<UserGuildMasterViewModel> Users { get; set; }

        public IEnumerable<RaidDestinationGuildMasterProgressViewModel> RaidDestinations { get; set; }
    }
}
