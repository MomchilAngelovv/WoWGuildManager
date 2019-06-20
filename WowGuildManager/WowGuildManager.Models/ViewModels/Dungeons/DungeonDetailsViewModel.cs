using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Models.ViewModels.Dungeons
{
    public class DungeonDetailsViewModel
    {
        public string Id { get; set; }

        public List<CharacterViewModel> Characters { get; set; }

        public List<CharacterJoinViewModel> AvailableCharacters { get; set; }
        //TODO: Make characters unique names
    }
}
