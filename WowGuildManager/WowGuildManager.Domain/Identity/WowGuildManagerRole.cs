using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WowGuildManager.Domain.Identity
{
    public class WowGuildManagerRole : IdentityRole<string>
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
