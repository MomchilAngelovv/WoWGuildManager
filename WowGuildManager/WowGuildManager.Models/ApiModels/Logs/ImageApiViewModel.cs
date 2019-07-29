namespace WowGuildManager.Models.ApiModels.Logs
{
    using System;

    public class ImageApiViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Format { get; set; }

        public long Length { get; set; }

        public string UserId { get; set; }

        public string Url { get; set; }

        public bool IsActual { get; set; }
    }
}
