namespace WowGuildManager.Models.ViewModels.Raids
{
    using System;
    using System.Collections.Generic;

    using WowGuildManager.Models.ViewModels.Characters;

    public class RaidDetailsViewModel
    {
        public string Id { get; set; }

        public DateTime EventDateTime { get; set; }

        public string Destination { get; set; }

        public string Description { get; set; }

        public int MaxPlayers { get; set; }

        public string LeaderId { get; set; }

        public IEnumerable<CharacterRaidDetailsViewModel> Characters { get; set; }

        public IEnumerable<CharacterIdNameViewModel> AvailableCharacters { get; set; }

        public bool AlreadyJoined { get; set; }

        public CharacterIdNameViewModel JoinedCharacter { get; set; }
    }
}
