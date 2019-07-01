namespace WowGuildManager.Models.ApiModels.Members
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ApiModels.Characters;
    public class MembersViewModel
    {
        public IEnumerable<CharacterApiViewModel> Members { get; set; }
    }
}
