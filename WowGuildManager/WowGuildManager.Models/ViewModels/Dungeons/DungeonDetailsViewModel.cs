namespace WowGuildManager.Models.ViewModels.Dungeons
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Characters;
    public class DungeonDetailsViewModel
    {
        public string Id { get; set; }

        public IEnumerable<CharacterDungeonDetailsViewModel> Characters { get; set; }

        public IEnumerable<CharacterIdNameViewModel> AvailableCharacters { get; set; }

        public bool AlreadyJoined { get; set; }

        public CharacterIdNameViewModel JoinedCharacter { get; set; }
    }
}
