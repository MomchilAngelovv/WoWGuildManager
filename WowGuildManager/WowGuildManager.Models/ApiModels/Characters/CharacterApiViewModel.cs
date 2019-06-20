namespace WowGuildManager.Models.ApiModels.Characters
{
    public class CharacterApiViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Class { get; set; }

        public int Level { get; set; }

        public int Role { get; set; }

        public string Image { get; set; }

        public int GuildPoints { get; set; }

        public string WowGuildManagerUserId { get; set; }

        public string[] Raids { get; set; }

        public string[] Dungeons { get; set; }
    }
}
