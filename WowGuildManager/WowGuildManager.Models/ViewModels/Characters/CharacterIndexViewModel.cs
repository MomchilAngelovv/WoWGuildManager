namespace WowGuildManager.Models.ViewModels.Characters
{
    using System.Collections.Generic;

    public class CharacterIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Characters { get; set; }
    }
}
