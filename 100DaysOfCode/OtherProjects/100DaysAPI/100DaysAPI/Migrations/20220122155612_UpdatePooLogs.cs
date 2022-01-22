using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _100DaysAPI.Migrations
{
    public partial class UpdatePooLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PooLog",
                table: "PooLog");

            migrationBuilder.RenameTable(
                name: "PooLog",
                newName: "PooLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PooLogs",
                table: "PooLogs",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PooLogs",
                table: "PooLogs");

            migrationBuilder.RenameTable(
                name: "PooLogs",
                newName: "PooLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PooLog",
                table: "PooLog",
                column: "ID");
        }
    }
}
