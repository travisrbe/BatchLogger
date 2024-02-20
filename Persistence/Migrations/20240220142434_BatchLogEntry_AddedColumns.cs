using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BatchLogEntry_AddedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SpecificGravityReading",
                table: "BatchLogEntry",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "pHReading",
                table: "BatchLogEntry",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecificGravityReading",
                table: "BatchLogEntry");

            migrationBuilder.DropColumn(
                name: "pHReading",
                table: "BatchLogEntry");
        }
    }
}
