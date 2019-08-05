namespace WowGuildManager.Domain.Logs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Identity;

    public class Error
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual WowGuildManagerUser User { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
