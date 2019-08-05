namespace WowGuildManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Added_IsActual_Property_To_ImageUploadLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsedId",
                table: "ImageUploadLogs",
                newName: "UserId");

            migrationBuilder.AlterColumn<long>(
                name: "Length",
                table: "ImageUploadLogs",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "ImageUploadLogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "ImageUploadLogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ImageUploadLogs",
                newName: "UsedId");

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "ImageUploadLogs",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
