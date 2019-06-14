using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WowGuildManager.Domain.Raid;

namespace WowGuildManager.Models.ViewModels.Raids
{
    public class RaidCreateInputModel
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public RaidPlace Place { get; set; }

        public string Description { get; set; }

        [Required]
        public string RaidLeaderId { get; set; }
    }
}
