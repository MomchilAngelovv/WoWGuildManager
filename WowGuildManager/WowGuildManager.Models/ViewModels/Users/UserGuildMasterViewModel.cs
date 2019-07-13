using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Users
{
    public class UserGuildMasterViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsRaidLeader { get; set; }

        public bool IsGuildMaster { get; set; }
    }
}
