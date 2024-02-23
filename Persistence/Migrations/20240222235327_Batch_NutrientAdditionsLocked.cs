using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Batch_NutrientAdditionsLocked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complete",
                table: "Batch",
                newName: "IsComplete");

            migrationBuilder.AddColumn<bool>(
                name: "NutrientAdditionsLocked",
                table: "Batch",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NutrientAdditionsLocked",
                table: "Batch");

            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "Batch",
                newName: "Complete");
        }
    }
}
