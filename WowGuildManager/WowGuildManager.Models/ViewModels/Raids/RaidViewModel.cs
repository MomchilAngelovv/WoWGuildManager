namespace WowGuildManager.Models.ViewModels.Raids
{
    using WowGuildManager.Models.ViewModels.Characters;

    public class RaidViewModel
    {
        public string Id { get; set; }

        public string EventDateTime { get; set; }

        public string Image { get; set; }

        public string Destination { get; set; }

        public string Description { get; set; }

        public string LeaderName { get; set; }

        public string RegisteredPlayers { get; set; }

        public string MaxPlayers { get; set; }

        public bool AlreadyJoined { get; set; }

        public CharacterNameRoleViewModel JoinedCharacter { get; set; }

        public bool IsLeader => this.JoinedCharacter.Name == this.LeaderName;
    }
}
