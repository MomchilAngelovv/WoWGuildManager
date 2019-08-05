namespace WowGuildManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Added_Character_Roles_And_Guild_Ranks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "CharacterClasses",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "CharacterClasses");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Characters",
                nullable: false,
                defaultValue: "");
        }
    }
}
