using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PresetLookup_FOREIGNKEYCONSTRAINTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.StackPresetLookup ADD CONSTRAINT UQ_STACKPRESETLOOKUP UNIQUE(StackPresetId, Priority);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.StackPresetLookup DROP CONSTRAINT UQ_STACKPRESETLOOKUP;");
        }
    }
}
