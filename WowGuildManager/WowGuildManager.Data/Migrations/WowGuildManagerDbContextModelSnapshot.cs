﻿namespace WowGuildManager.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [DbContext(typeof(WowGuildManagerDbContext))]
    partial class WowGuildManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Characters.Character", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClassId")
                        .IsRequired();

                    b.Property<int>("GuildPoints");

                    b.Property<string>("GuildRankId")
                        .IsRequired();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.Property<string>("WowGuildManagerUserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("GuildRankId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("WowGuildManagerUserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Characters.CharacterClass", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CharacterClasses");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Characters.CharacterRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CharacterRoles");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Characters.GuildRank", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GuildRanks");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.Dungeon", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("DestinationId")
                        .IsRequired();

                    b.Property<DateTime>("EventDateTime");

                    b.Property<string>("LeaderId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Dungeons");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.DungeonCharacter", b =>
                {
                    b.Property<string>("DungeonId");

                    b.Property<string>("CharacterId");

                    b.HasKey("DungeonId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("DungeonCharacter");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.DungeonDestination", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<int>("MaxPlayers");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DungeonDestinations");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Identity.WowGuildManagerRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Identity.WowGuildManagerUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsGuildMaster");

                    b.Property<bool>("IsRaidLeader");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.Raid", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("DestinationId")
                        .IsRequired();

                    b.Property<DateTime>("EventDateTime");

                    b.Property<string>("LeaderId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Raids");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.RaidCharacter", b =>
                {
                    b.Property<string>("RaidId");

                    b.Property<string>("CharacterId");

                    b.HasKey("RaidId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("RaidCharacter");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.RaidDestination", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<int>("KilledBosses");

                    b.Property<int>("MaxPlayers");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TotalBosses");

                    b.HasKey("Id");

                    b.ToTable("RaidDestinations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Characters.Character", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.CharacterClass", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Characters.GuildRank", "GuildRank")
                        .WithMany()
                        .HasForeignKey("GuildRankId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Characters.CharacterRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser", "User")
                        .WithMany("Characters")
                        .HasForeignKey("WowGuildManagerUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.Dungeon", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Dungeon.DungeonDestination", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.DungeonCharacter", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Character")
                        .WithMany("Dungeons")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WowGuildManager.Domain.Dungeon.Dungeon", "Dungeon")
                        .WithMany("RegisteredCharacters")
                        .HasForeignKey("DungeonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.Raid", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Raid.RaidDestination", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.RaidCharacter", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Character")
                        .WithMany("Raids")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WowGuildManager.Domain.Raid.Raid", "Raid")
                        .WithMany("RegisteredCharacters")
                        .HasForeignKey("RaidId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
