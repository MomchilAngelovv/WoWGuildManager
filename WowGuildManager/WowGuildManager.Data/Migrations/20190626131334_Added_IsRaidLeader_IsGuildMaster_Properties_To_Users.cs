using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Added_IsRaidLeader_IsGuildMaster_Properties_To_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGuildMaster",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRaidLeader",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGuildMaster",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsRaidLeader",
                table: "AspNetUsers");
        }
    }
}
