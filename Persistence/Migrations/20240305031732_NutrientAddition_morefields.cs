using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NutrientAddition_morefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GramsToAdd",
                table: "NutrientAddition",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOverride",
                table: "NutrientAddition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GramsToAdd",
                table: "NutrientAddition");

            migrationBuilder.DropColumn(
                name: "NameOverride",
                table: "NutrientAddition");
        }
    }
}
