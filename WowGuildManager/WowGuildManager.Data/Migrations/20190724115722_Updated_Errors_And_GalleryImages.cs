namespace WowGuildManager.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Updated_Errors_And_GalleryImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GuildRanks_GuildRankId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "GuildRanks");

            migrationBuilder.DropTable(
                name: "ImageUploadLogs");

            migrationBuilder.RenameColumn(
                name: "GuildRankId",
                table: "Characters",
                newName: "RankId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_GuildRankId",
                table: "Characters",
                newName: "IX_Characters_RankId");

            migrationBuilder.CreateTable(
                name: "CharacterRanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Errors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Format = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    IsActual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Errors_UserId",
                table: "Errors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_UserId",
                table: "GalleryImages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterRanks_RankId",
                table: "Characters",
                column: "RankId",
                principalTable: "CharacterRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterRanks_RankId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "CharacterRanks");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "GalleryImages");

            migrationBuilder.RenameColumn(
                name: "RankId",
                table: "Characters",
                newName: "GuildRankId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_RankId",
                table: "Characters",
                newName: "IX_Characters_GuildRankId");

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: false),
                    ExceptionTime = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuildRanks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageUploadLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Format = table.Column<string>(nullable: false),
                    IsActual = table.Column<bool>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUploadLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GuildRanks_GuildRankId",
                table: "Characters",
                column: "GuildRankId",
                principalTable: "GuildRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
