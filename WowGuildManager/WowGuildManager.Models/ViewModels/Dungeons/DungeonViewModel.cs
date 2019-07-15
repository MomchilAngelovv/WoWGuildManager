namespace WowGuildManager.Models.ViewModels.Dungeons
{
    using WowGuildManager.Models.ViewModels.Characters;

    public class DungeonViewModel
    {
        public string Id { get; set; }

        public string DateTime { get; set; }

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
