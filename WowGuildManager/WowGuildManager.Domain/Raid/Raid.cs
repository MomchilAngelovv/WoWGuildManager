using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Raid
{

    //TODO: Validation
    public class Raid
    {
        public Raid()
        {
            this.RegisteredCharacters = new HashSet<Character>();
        }

        public string Id { get; set; }

        public RaidPlace Place { get; set; }

        public int MaxPlayers { get; set; }

        public int RegisteredPlayers => this.RegisteredCharacters.Count;

        public string CharacterId { get; set; }
        public Character RaidLeader { get; set; }

        public ICollection<Character> RegisteredCharacters { get; set; }
    }
}
