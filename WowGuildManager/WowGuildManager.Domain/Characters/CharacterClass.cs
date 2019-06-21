﻿using System.ComponentModel.DataAnnotations;

namespace WowGuildManager.Domain.Characters
{
    public class CharacterClass
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
