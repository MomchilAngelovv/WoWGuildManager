﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WowGuildManager.Domain.Raid
{
    //TODO:Validate every domein entity
    public class RaidDestination
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [Range(10,40)]
        public int MaxPlayers { get; set; }
    }
}   