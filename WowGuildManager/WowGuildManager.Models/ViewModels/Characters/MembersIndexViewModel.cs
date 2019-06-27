using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WowGuildManager.Models.ViewModels.Characters
{
    public class MembersIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Members { get; set; }

        public int MembersCount => this.Members.Count();
    }
}
