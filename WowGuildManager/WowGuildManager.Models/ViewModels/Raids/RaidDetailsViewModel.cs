using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Models.ViewModels.Raids
{
    public class RaidDetailsViewModel
    {
        public string Id { get; set; }

        public IEnumerable<CharacterRaidDetailsViewModel> Characters { get; set; }

        public IEnumerable<CharacterIdNameViewModel> AvailableCharacters { get; set; }

        public bool AlreadyJoined { get; set; }

        public CharacterIdNameViewModel JoinedCharacter { get; set; }
    }
}
