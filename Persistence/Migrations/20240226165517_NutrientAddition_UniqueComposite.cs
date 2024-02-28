using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NutrientAddition_UniqueComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //EF Core will not do this out of the box without custom annotations.
            //See: https://stackoverflow.com/questions/49526370/is-there-a-data-annotation-for-unique-constraint-in-ef-core-code-first
            //If this comes up again, consider doing this.
            migrationBuilder.Sql(
                "ALTER TABLE dbo.NutrientAddition ADD CONSTRAINT UQ_NUTRIENTADDITION UNIQUE(BatchId, Priority);"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE dbo.NutrientAddition DROP CONSTRAINT UQ_NUTRIENTADDITION;"
            );
        }
    }
}
