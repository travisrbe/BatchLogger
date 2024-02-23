using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class User_Autogen_Create_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorToken",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.Sql(
                @"
CREATE TRIGGER [dbo].[User_UPDATE] ON [dbo].[User]
AFTER UPDATE
AS
BEGIN
SET NOCOUNT ON;

IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

DECLARE @Id NVARCHAR(450)

SELECT @Id = INSERTED.Id
FROM INSERTED

UPDATE [dbo].[User]
SET [UpdateDate] = GETUTCDATE()
WHERE Id = @Id
END

GO"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorToken",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.Sql(@"
DROP TRIGGER [dbo].[User_UPDATE]

GO"
            );
        }
    }
}
