using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Characters
{
    public class CharacterDungeonDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Level { get; set; }

        public string Role { get; set; }

        public string Class { get; set; }

        public string GuildRank { get; set; }
    }
}
