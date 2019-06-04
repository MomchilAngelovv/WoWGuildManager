using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<RaidCharacters> RaidCharacters { get; set; }

        public DbSet<DungeonCharacters> DungeonCharacters { get; set; }

        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DungeonCharacters>()
                .HasKey(e => new { e.DungeonId, e.CharacterId });

            builder.Entity<RaidCharacters>()
               .HasKey(e => new { e.RaidnId, e.CharacterId });

            base.OnModelCreating(builder);
        }
    }
}
