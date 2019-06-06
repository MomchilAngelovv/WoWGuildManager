using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Characters
{
    public class MembersIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Members { get; set; }
    }
}
