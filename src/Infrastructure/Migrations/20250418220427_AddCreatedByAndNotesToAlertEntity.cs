using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibeTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByAndNotesToAlertEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Alerts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Alerts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Alerts");
        }
    }
}
