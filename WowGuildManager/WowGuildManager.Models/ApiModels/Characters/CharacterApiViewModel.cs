namespace WowGuildManager.Models.ApiModels.Characters
{
    public class CharacterApiViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }

        public int Level { get; set; }

        public string Role { get; set; }

        public string GuildRank { get; set; }

        public int GuildPoints { get; set; }

        public string WowGuildManagerUserId { get; set; }

        public string[] Raids { get; set; }

        public string[] Dungeons { get; set; }
    }
}
