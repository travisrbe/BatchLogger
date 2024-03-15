using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StackPresets_and_Lookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StackPreset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StackPreset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StackPresetLookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StackPresetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NutrientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StackPresetLookup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StackPresetLookup_Nutrient_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StackPresetLookup_StackPreset_StackPresetId",
                        column: x => x.StackPresetId,
                        principalTable: "StackPreset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StackPresetLookup_NutrientId",
                table: "StackPresetLookup",
                column: "NutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_StackPresetLookup_StackPresetId",
                table: "StackPresetLookup",
                column: "StackPresetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StackPresetLookup");

            migrationBuilder.DropTable(
                name: "StackPreset");
        }
    }
}
