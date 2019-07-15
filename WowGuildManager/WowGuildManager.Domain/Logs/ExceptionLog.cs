namespace WowGuildManager.Domain.Logs
{
    using System;

    using System.ComponentModel.DataAnnotations;

    public class ExceptionLog
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ExceptionMessage { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime ExceptionTime { get; set; }
    }
}
