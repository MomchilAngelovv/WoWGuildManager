using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Raid;

namespace WowGuildManager.Data.Configurations
{
    public class RaidCharacterConfiguration : IEntityTypeConfiguration<RaidCharacter>
    {
        public void Configure(EntityTypeBuilder<RaidCharacter> entity)
        {
            entity
                .HasKey(raidChar => new { raidChar.RaidId, raidChar.CharacterId });

            entity
              .HasOne(raidChar => raidChar.Raid)
              .WithMany(raid => raid.RegisteredCharacters)
              .HasForeignKey(raidChar => raidChar.RaidId)
              .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(raidChar => raidChar.Character)
                .WithMany(raid => raid.Raids)
                .HasForeignKey(raidChar => raidChar.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
