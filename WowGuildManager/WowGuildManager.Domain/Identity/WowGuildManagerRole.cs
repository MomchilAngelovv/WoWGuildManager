namespace WowGuildManager.Domain.Identity
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using WowGuildManager.Common.GlobalConstants;

    public class WowGuildManagerRole : IdentityRole<string>
    {
        [Required]
        [MaxLength(CommonConstants.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
