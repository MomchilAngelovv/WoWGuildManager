﻿namespace WowGuildManager.Domain.Dungeon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Characters;
    public class Dungeon
    {
        public Dungeon()
        {
            this.RegisteredCharacters = new HashSet<DungeonCharacter>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime EventDateTime { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string DestinationId { get; set; }
        public virtual DungeonDestination Destination { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public virtual Character Leader { get; set; }

        public virtual ICollection<DungeonCharacter> RegisteredCharacters { get; set; }
    }
}
