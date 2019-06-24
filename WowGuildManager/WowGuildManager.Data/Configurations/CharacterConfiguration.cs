using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Data.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> entity)
        {
            entity
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
