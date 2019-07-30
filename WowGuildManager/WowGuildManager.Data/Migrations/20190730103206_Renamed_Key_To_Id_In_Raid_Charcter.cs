using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Renamed_Key_To_Id_In_Raid_Charcter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "RaidCharacter",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RaidCharacter",
                newName: "Key");
        }
    }
}
