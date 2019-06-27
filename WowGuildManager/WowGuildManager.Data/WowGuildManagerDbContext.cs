using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WowGuildManager.Data.Configurations;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Domain.Raid;

namespace WowGuildManager.Data
{
    public class WowGuildManagerDbContext : IdentityDbContext<WowGuildManagerUser, WowGuildManagerRole, string>
    {
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

        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CharacterConfiguration());
            builder.ApplyConfiguration(new DungeonCharacterConfiguration());
            builder.ApplyConfiguration(new RaidCharacterConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
