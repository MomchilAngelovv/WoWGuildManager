namespace WowGuildManager.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using WowGuildManager.Domain.Raid;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Domain.Dungeon;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Domain.Characters;
    using WowGuildManager.Data.Configurations;

    public class WowGuildManagerDbContext : IdentityDbContext<WowGuildManagerUser, WowGuildManagerRole, string>
    {
        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
           : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<Dungeon> Dungeons { get; set; }

        public DbSet<Raid> Raids { get; set; }

        public DbSet<RaidCharacter> RaidCharacter { get; set; }

        public DbSet<DungeonCharacter> DungeonCharacter { get; set; }

        public DbSet<CharacterClass> CharacterClasses { get; set; }

        public DbSet<CharacterRole> CharacterRoles { get; set; }

        public DbSet<DungeonDestination> DungeonDestinations { get; set; }

        public DbSet<RaidDestination> RaidDestinations { get; set; }

        public DbSet<GuildRank> GuildRanks { get; set; }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CharacterConfiguration());
            builder.ApplyConfiguration(new DungeonCharacterConfiguration());
            builder.ApplyConfiguration(new RaidCharacterConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
