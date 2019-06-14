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

        public DbSet<RaidsCharacters> RaidsCharacters { get; set; }

        public DbSet<DungeonsCharacters> DungeonsCharacters { get; set; }

        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DungeonsCharacters>()
                .HasKey(e => new { e.DungeonId, e.CharacterId });

            builder.Entity<DungeonsCharacters>().HasOne(dc => dc.Dungeon)
                .WithMany(d => d.RegisteredCharacters)
                .HasForeignKey(d => d.DungeonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DungeonsCharacters>().HasOne(dc => dc.Character)
               .WithMany(d => d.Dungeons)
               .HasForeignKey(d => d.CharacterId)
               .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<RaidsCharacters>()
               .HasKey(e => new { e.RaidId, e.CharacterId });

            builder.Entity<RaidsCharacters>().HasOne(dc => dc.Raid)
               .WithMany(d => d.RegisteredCharacters)
               .HasForeignKey(d => d.RaidId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RaidsCharacters>().HasOne(dc => dc.Character)
            .WithMany(d => d.Raids)
            .HasForeignKey(d => d.CharacterId)
            .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
