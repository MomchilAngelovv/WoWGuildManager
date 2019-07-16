using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Added_ImageUploadLogs_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_WowGuildManagerUserId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "WowGuildManagerUserId",
                table: "Characters",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_WowGuildManagerUserId",
                table: "Characters",
                newName: "IX_Characters_UserId");

            migrationBuilder.CreateTable(
                name: "ImageUploadLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Format = table.Column<string>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUploadLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "ImageUploadLogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Characters",
                newName: "WowGuildManagerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                newName: "IX_Characters_WowGuildManagerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_AspNetUsers_WowGuildManagerUserId",
                table: "Characters",
                column: "WowGuildManagerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
