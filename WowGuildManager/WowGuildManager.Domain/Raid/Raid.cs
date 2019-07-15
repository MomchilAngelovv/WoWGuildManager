﻿namespace WowGuildManager.Domain.Raid
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WowGuildManager.Domain.Characters;

    public class Raid
    {
        public Raid()
        {
            this.RegisteredCharacters = new HashSet<RaidCharacter>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime EventDateTime { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string DestinationId { get; set; }
        public virtual RaidDestination Destination { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public virtual Character Leader { get; set; }

        public virtual ICollection<RaidCharacter> RegisteredCharacters { get; set; }
    }
}
