using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Database_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Raids",
                newName: "DestinationId");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Raids",
                newName: "EventDateTime");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Dungeons",
                newName: "DestinationId");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Dungeons",
                newName: "EventDateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Raids",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Raids",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dungeons",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Dungeons",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                table: "Characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuildRankId",
                table: "Characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "CharacterClasses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DungeonDestinations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonDestinations", x => x.Id);
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
                name: "RaidDestinations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaidDestinations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Raids_DestinationId",
                table: "Raids",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeons_DestinationId",
                table: "Dungeons",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ClassId",
                table: "Characters",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GuildRankId",
                table: "Characters",
                column: "GuildRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RoleId",
                table: "Characters",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterClasses_ClassId",
                table: "Characters",
                column: "ClassId",
                principalTable: "CharacterClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GuildRanks_GuildRankId",
                table: "Characters",
                column: "GuildRankId",
                principalTable: "GuildRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterRoles_RoleId",
                table: "Characters",
                column: "RoleId",
                principalTable: "CharacterRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dungeons_DungeonDestinations_DestinationId",
                table: "Dungeons",
                column: "DestinationId",
                principalTable: "DungeonDestinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Raids_RaidDestinations_DestinationId",
                table: "Raids",
                column: "DestinationId",
                principalTable: "RaidDestinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterClasses_ClassId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GuildRanks_GuildRankId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterRoles_RoleId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Dungeons_DungeonDestinations_DestinationId",
                table: "Dungeons");

            migrationBuilder.DropForeignKey(
                name: "FK_Raids_RaidDestinations_DestinationId",
                table: "Raids");

            migrationBuilder.DropTable(
                name: "CharacterClasses");

            migrationBuilder.DropTable(
                name: "CharacterRoles");

            migrationBuilder.DropTable(
                name: "DungeonDestinations");

            migrationBuilder.DropTable(
                name: "GuildRanks");

            migrationBuilder.DropTable(
                name: "RaidDestinations");

            migrationBuilder.DropIndex(
                name: "IX_Raids_DestinationId",
                table: "Raids");

            migrationBuilder.DropIndex(
                name: "IX_Dungeons_DestinationId",
                table: "Dungeons");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ClassId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_GuildRankId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_RoleId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GuildRankId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "EventDateTime",
                table: "Raids",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Raids",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "EventDateTime",
                table: "Dungeons",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Dungeons",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Raids",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Raids",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Raids",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "Raids",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dungeons",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Dungeons",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Dungeons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "Dungeons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
