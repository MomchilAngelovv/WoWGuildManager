using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Models.ViewModels.Characters
{
    public class CharacterCreateInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }

        [Range(1, 60)]
        public int Level { get; set; }

        [Required]
        public string Role { get; set; }

        //TODO: FIx identity area bootstrap and interface
        //TODO: FIX CSS LAST
        public string UserId { get; set; }
    }
}
