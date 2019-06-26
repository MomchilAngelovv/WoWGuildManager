using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ApiModels.Characters;

namespace WowGuildManager.Models.ApiModels.Members
{
    public class MembersViewModel
    {
        public IEnumerable<CharacterApiViewModel> Members { get; set; }
    }
}
