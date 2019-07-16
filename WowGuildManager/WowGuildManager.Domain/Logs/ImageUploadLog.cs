namespace WowGuildManager.Domain.Logs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImageUploadLog
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Format { get; set; }

        [Range(1,long.MaxValue)]
        public long Length { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Url { get; set; }

        public bool IsActual { get; set; }
    }
}
