namespace WowGuildManager.Models.ViewModels.Characters
{
    using System.Linq;
    using System.Collections.Generic;

    public class MembersIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Members { get; set; }

        public int MembersCount { get; set; }
    }
}
