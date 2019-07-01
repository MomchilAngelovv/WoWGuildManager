namespace WowGuildManager.Domain.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    using WowGuildManager.Domain.Characters;
    public class WowGuildManagerUser : IdentityUser<string>
    {
        public bool IsRaidLeader { get; set; }

        public bool IsGuildMaster { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
