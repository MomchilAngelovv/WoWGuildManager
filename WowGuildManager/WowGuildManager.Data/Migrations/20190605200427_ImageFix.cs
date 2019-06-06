using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Web.Migrations
{
    public partial class ImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Image",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
