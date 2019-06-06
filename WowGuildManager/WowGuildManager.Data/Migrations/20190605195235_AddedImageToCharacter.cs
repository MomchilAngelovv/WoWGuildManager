using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Web.Migrations
{
    public partial class AddedImageToCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Image",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Characters");
        }
    }
}
