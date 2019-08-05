namespace WowGuildManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Updated_Exception_Logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExceptionLogs_AspNetUsers_WowGuildManagerUserId",
                table: "ExceptionLogs");

            migrationBuilder.DropIndex(
                name: "IX_ExceptionLogs_WowGuildManagerUserId",
                table: "ExceptionLogs");

            migrationBuilder.RenameColumn(
                name: "WowGuildManagerUserId",
                table: "ExceptionLogs",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "ExceptionLogs",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "ExceptionLogs",
                newName: "WowGuildManagerUserId");

            migrationBuilder.AlterColumn<string>(
                name: "WowGuildManagerUserId",
                table: "ExceptionLogs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLogs_WowGuildManagerUserId",
                table: "ExceptionLogs",
                column: "WowGuildManagerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExceptionLogs_AspNetUsers_WowGuildManagerUserId",
                table: "ExceptionLogs",
                column: "WowGuildManagerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
