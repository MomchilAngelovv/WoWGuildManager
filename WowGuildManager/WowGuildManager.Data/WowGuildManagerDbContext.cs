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

        public DbSet<RaidCharacter> RaidCharacter { get; set; }

        public DbSet<DungeonCharacter> DungeonCharacter { get; set; }

        public DbSet<CharacterClass> CharacterClasses { get; set; }

        public DbSet<CharacterRole> CharacterRoles { get; set; }

        public DbSet<DungeonDestination> DungeonDestinations { get; set; }

        public DbSet<RaidDestination> RaidDestinations { get; set; }

        public DbSet<GuildRank> GuildRanks { get; set; }

        //TODO:Move a lot of stuf into common constants
        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DungeonCharacter>().HasKey(dungChar => new { dungChar.DungeonId, dungChar.CharacterId });
            builder.Entity<RaidCharacter>().HasKey(raidChar => new { raidChar.RaidId, raidChar.CharacterId });

            builder.Entity<Character>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<DungeonCharacter>()
                .HasOne(dungChar => dungChar.Dungeon)
                .WithMany(dung => dung.RegisteredCharacters)
                .HasForeignKey(dungChar => dungChar.DungeonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DungeonCharacter>()
                .HasOne(dungChar => dungChar.Character)
                .WithMany(dung => dung.Dungeons)
                .HasForeignKey(dungChar => dungChar.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Entity<RaidCharacter>()
                .HasOne(raidChar => raidChar.Raid)
                .WithMany(raid => raid.RegisteredCharacters)
                .HasForeignKey(raidChar => raidChar.RaidId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RaidCharacter>()
                .HasOne(raidChar => raidChar.Character)
                .WithMany(raid => raid.Raids)
                .HasForeignKey(raidChar => raidChar.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
