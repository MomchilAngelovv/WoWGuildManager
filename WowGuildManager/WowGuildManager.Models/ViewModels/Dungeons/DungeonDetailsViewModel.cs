using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Characters;

namespace WowGuildManager.Models.ViewModels.Dungeons
{
    public class DungeonDetailsViewModel
    {
        public string Id { get; set; }

        public IEnumerable<CharacterDungeonDetailsViewModel> Characters { get; set; }

        public IEnumerable<CharacterIdNameViewModel> AvailableCharacters { get; set; }

        public bool AlreadyJoined { get; set; }

        public string JoinedCharacterName { get; set; }

        public string JoinedCharacterId { get; set; }
    }
}
