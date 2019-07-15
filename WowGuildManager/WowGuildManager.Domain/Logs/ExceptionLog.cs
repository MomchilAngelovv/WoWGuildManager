using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Domain.Logs
{
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
