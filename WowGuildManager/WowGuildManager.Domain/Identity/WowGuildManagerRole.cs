namespace WowGuildManager.Domain.Identity
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    public class WowGuildManagerRole : IdentityRole<string>
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
