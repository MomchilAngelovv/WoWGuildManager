using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Domain.Identity
{
    public class WowGuildManagerUser : IdentityUser<string>
    {
        public virtual ICollection<Character> Characters { get; set; }
    }
}
