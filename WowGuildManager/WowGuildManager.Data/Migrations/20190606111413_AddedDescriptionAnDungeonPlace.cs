using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Web.Migrations
{
    public partial class AddedDescriptionAnDungeonPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Raids",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dungeons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "Dungeons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Dungeons");
        }
    }
}
