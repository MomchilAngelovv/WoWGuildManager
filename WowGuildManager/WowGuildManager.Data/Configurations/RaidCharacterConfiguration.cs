namespace WowGuildManager.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using WowGuildManager.Domain.Raid;

    public class RaidCharacterConfiguration : IEntityTypeConfiguration<RaidCharacter>
    {
        public void Configure(EntityTypeBuilder<RaidCharacter> raidCharacter)
        {
            raidCharacter
              .HasOne(raidChar => raidChar.Raid)
              .WithMany(raid => raid.RegisteredCharacters)
              .HasForeignKey(raidChar => raidChar.RaidId)
              .OnDelete(DeleteBehavior.Restrict);

            raidCharacter
                .HasOne(raidChar => raidChar.Character)
                .WithMany(raid => raid.Raids)
                .HasForeignKey(raidChar => raidChar.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
