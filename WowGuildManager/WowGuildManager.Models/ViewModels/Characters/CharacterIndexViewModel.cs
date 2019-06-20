using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Characters
{
    //TODO: Make services return mapped entities with IEnumbeale
    public class CharacterIndexViewModel
    {
        public IEnumerable<CharacterViewModel> Characters { get; set; }
    }
}
