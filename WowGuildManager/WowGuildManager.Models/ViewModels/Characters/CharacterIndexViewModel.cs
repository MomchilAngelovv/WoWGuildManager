using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Characters
{
    public class CharacterIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Characters { get; set; }
    }
}
