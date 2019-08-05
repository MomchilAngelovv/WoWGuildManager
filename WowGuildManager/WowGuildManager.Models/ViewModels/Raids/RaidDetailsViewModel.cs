namespace WowGuildManager.Models.ViewModels.Raids
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using WowGuildManager.Common.GlobalConstants;
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

        public int HealersCount => this.Characters.Count(cha => cha.Role == CharacterRoleConstants.Healer);
        public int DamageDealersCount => this.Characters.Count(cha => cha.Role == CharacterRoleConstants.Damage);
        public int TanksCount => this.Characters.Count(cha => cha.Role == CharacterRoleConstants.Tank);
    }
}
