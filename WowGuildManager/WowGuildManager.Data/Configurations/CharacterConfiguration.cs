namespace WowGuildManager.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using WowGuildManager.Domain.Characters;

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
