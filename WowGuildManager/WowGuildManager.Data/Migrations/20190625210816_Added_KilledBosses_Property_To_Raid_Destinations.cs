namespace WowGuildManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Added_KilledBosses_Property_To_Raid_Destinations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KilledBosses",
                table: "RaidDestinations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KilledBosses",
                table: "RaidDestinations");
        }
    }
}
