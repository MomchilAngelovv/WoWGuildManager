using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Domain.Characters
{
    //TODO: Validation
    public class Character
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ClassType Class { get; set; }

        public int Level { get; set; }

        public CharacterRole Role { get; set; }

        public string WowGuildManagerUserId { get; set; }
        public WowGuildManagerUser User { get; set; }
    }
}
