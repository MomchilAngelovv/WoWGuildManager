using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Added_Is_Active_On_Characters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RaidCharacter",
                table: "RaidCharacter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DungeonCharacter",
                table: "DungeonCharacter");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterId",
                table: "RaidCharacter",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RaidId",
                table: "RaidCharacter",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "RaidCharacter",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterId",
                table: "DungeonCharacter",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DungeonId",
                table: "DungeonCharacter",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DungeonCharacter",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Characters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaidCharacter",
                table: "RaidCharacter",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DungeonCharacter",
                table: "DungeonCharacter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RaidCharacter_RaidId",
                table: "RaidCharacter",
                column: "RaidId");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonCharacter_DungeonId",
                table: "DungeonCharacter",
                column: "DungeonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RaidCharacter",
                table: "RaidCharacter");

            migrationBuilder.DropIndex(
                name: "IX_RaidCharacter_RaidId",
                table: "RaidCharacter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DungeonCharacter",
                table: "DungeonCharacter");

            migrationBuilder.DropIndex(
                name: "IX_DungeonCharacter_DungeonId",
                table: "DungeonCharacter");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "RaidCharacter");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DungeonCharacter");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "RaidId",
                table: "RaidCharacter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharacterId",
                table: "RaidCharacter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DungeonId",
                table: "DungeonCharacter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharacterId",
                table: "DungeonCharacter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaidCharacter",
                table: "RaidCharacter",
                columns: new[] { "RaidId", "CharacterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DungeonCharacter",
                table: "DungeonCharacter",
                columns: new[] { "DungeonId", "CharacterId" });
        }
    }
}
