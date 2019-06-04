using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Web.Migrations
{
    public partial class AddedCharacterDungeonRaidEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Class = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    WowGuildManagerUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_AspNetUsers_WowGuildManagerUserId",
                        column: x => x.WowGuildManagerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dungeons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    LeaderId = table.Column<string>(nullable: false),
                    DungeonLeaderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dungeons_Characters_DungeonLeaderId",
                        column: x => x.DungeonLeaderId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Raids",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Place = table.Column<int>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    LeaderId = table.Column<string>(nullable: false),
                    RaidLeaderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raids_Characters_RaidLeaderId",
                        column: x => x.RaidLeaderId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DungeonCharacters",
                columns: table => new
                {
                    DungeonId = table.Column<string>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonCharacters", x => new { x.DungeonId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_DungeonCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DungeonCharacters_Dungeons_DungeonId",
                        column: x => x.DungeonId,
                        principalTable: "Dungeons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaidCharacters",
                columns: table => new
                {
                    RaidnId = table.Column<string>(nullable: false),
                    CharacterId = table.Column<string>(nullable: false),
                    DungeonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaidCharacters", x => new { x.RaidnId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_RaidCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaidCharacters_Raids_DungeonId",
                        column: x => x.DungeonId,
                        principalTable: "Raids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_WowGuildManagerUserId",
                table: "Characters",
                column: "WowGuildManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonCharacters_CharacterId",
                table: "DungeonCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeons_DungeonLeaderId",
                table: "Dungeons",
                column: "DungeonLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RaidCharacters_CharacterId",
                table: "RaidCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_RaidCharacters_DungeonId",
                table: "RaidCharacters",
                column: "DungeonId");

            migrationBuilder.CreateIndex(
                name: "IX_Raids_RaidLeaderId",
                table: "Raids",
                column: "RaidLeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DungeonCharacters");

            migrationBuilder.DropTable(
                name: "RaidCharacters");

            migrationBuilder.DropTable(
                name: "Dungeons");

            migrationBuilder.DropTable(
                name: "Raids");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
