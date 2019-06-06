﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WowGuildManager.Data;

namespace WowGuildManager.Web.Migrations
{
    [DbContext(typeof(WowGuildManagerDbContext))]
    [Migration("20190606105307_AddedImagesAndDateTimeToRaidsAndDungeons")]
    partial class AddedImagesAndDateTimeToRaidsAndDungeons
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Class");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Role");

                    b.Property<string>("WowGuildManagerUserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("WowGuildManagerUserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.Dungeon", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("DungeonLeaderId");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("LeaderId")
                        .IsRequired();

                    b.Property<int>("MaxPlayers");

                    b.HasKey("Id");

                    b.HasIndex("DungeonLeaderId");

                    b.ToTable("Dungeons");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.DungeonCharacters", b =>
                {
                    b.Property<string>("DungeonId");

                    b.Property<string>("CharacterId");

                    b.HasKey("DungeonId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("DungeonCharacters");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Identity.WowGuildManagerRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description")
                        .IsRequired();

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

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("LeaderId")
                        .IsRequired();

                    b.Property<int>("MaxPlayers");

                    b.Property<int>("Place");

                    b.Property<string>("RaidLeaderId");

                    b.HasKey("Id");

                    b.HasIndex("RaidLeaderId");

                    b.ToTable("Raids");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.RaidCharacters", b =>
                {
                    b.Property<string>("RaidnId");

                    b.Property<string>("CharacterId");

                    b.Property<string>("DungeonId");

                    b.HasKey("RaidnId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("DungeonId");

                    b.ToTable("RaidCharacters");
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
                    b.HasOne("WowGuildManager.Domain.Identity.WowGuildManagerUser", "User")
                        .WithMany()
                        .HasForeignKey("WowGuildManagerUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.Dungeon", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "DungeonLeader")
                        .WithMany()
                        .HasForeignKey("DungeonLeaderId");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Dungeon.DungeonCharacters", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Character")
                        .WithMany("Dungeons")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Dungeon.Dungeon", "Dungeon")
                        .WithMany("RegisteredCharacters")
                        .HasForeignKey("DungeonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.Raid", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "RaidLeader")
                        .WithMany()
                        .HasForeignKey("RaidLeaderId");
                });

            modelBuilder.Entity("WowGuildManager.Domain.Raid.RaidCharacters", b =>
                {
                    b.HasOne("WowGuildManager.Domain.Characters.Character", "Character")
                        .WithMany("Raids")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WowGuildManager.Domain.Raid.Raid", "Dungeon")
                        .WithMany("RegisteredCharacters")
                        .HasForeignKey("DungeonId");
                });
#pragma warning restore 612, 618
        }
    }
}
