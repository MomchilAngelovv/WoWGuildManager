using Microsoft.EntityFrameworkCore.Migrations;

namespace WowGuildManager.Data.Migrations
{
    public partial class Add_Unique_Index_Characters_Name : Migration
    {
      
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characters_Name",
                table: "Characters");
        }
    }
}
