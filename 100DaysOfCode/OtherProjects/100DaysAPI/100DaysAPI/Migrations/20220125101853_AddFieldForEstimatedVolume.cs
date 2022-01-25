using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _100DaysAPI.Migrations
{
    public partial class AddFieldForEstimatedVolume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAmount",
                table: "MilkLogs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedAmount",
                table: "MilkLogs");
        }
    }
}
