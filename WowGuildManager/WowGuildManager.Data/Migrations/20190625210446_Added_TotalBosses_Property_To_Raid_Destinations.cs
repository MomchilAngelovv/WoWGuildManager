namespace WowGuildManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Added_TotalBosses_Property_To_Raid_Destinations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalBosses",
                table: "RaidDestinations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBosses",
                table: "RaidDestinations");
        }
    }
}
