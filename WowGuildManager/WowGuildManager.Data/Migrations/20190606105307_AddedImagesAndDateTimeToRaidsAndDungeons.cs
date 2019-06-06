using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Web.Migrations
{
    public partial class AddedImagesAndDateTimeToRaidsAndDungeons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Raids",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Raids",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Dungeons",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Dungeons",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Dungeons");
        }
    }
}
