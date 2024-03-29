﻿namespace WowGuildManager.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using WowGuildManager.Domain.Dungeon;

    public class DungeonCharacterConfiguration : IEntityTypeConfiguration<DungeonCharacter>
    {
        public void Configure(EntityTypeBuilder<DungeonCharacter> dungeonCharacter)
        {
            dungeonCharacter
              .HasOne(dungChar => dungChar.Dungeon)
              .WithMany(dung => dung.RegisteredCharacters)
              .HasForeignKey(dungChar => dungChar.DungeonId)
              .OnDelete(DeleteBehavior.Restrict);

            dungeonCharacter
                .HasOne(dungChar => dungChar.Character)
                .WithMany(dung => dung.Dungeons)
                .HasForeignKey(dungChar => dungChar.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
