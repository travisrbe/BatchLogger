using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_UPDATE_TRIGGERS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
CREATE TRIGGER [dbo].[BatchLogEntry_UPDATE] ON [dbo].[BatchLogEntry]
AFTER UPDATE
AS
BEGIN
SET NOCOUNT ON;

IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

DECLARE @Id UNIQUEIDENTIFIER

SELECT @Id = INSERTED.Id
FROM INSERTED

UPDATE dbo.BatchLogEntry
SET UpdateDate = GETUTCDATE()
WHERE Id = @Id
END

GO

CREATE TRIGGER [dbo].[Batch_UPDATE] ON [dbo].[Batch]
AFTER UPDATE
AS
BEGIN
SET NOCOUNT ON;

IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

DECLARE @Id UNIQUEIDENTIFIER

SELECT @Id = INSERTED.Id
FROM INSERTED

UPDATE dbo.Batch
SET UpdateDate = GETUTCDATE()
WHERE Id = @Id
END                

GO

CREATE TRIGGER [dbo].[BatchLogEntry_INSERT] ON [dbo].[BatchLogEntry]
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON;

IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

DECLARE @Id UNIQUEIDENTIFIER

SELECT @Id = INSERTED.Id
FROM INSERTED

UPDATE dbo.BatchLogEntry
SET CreateDate = GETUTCDATE(), UpdateDate = GETUTCDATE()
WHERE Id = @Id
END

GO

CREATE TRIGGER [dbo].[Batch_INSERT] ON [dbo].[Batch]
AFTER INSERT
AS
BEGIN
SET NOCOUNT ON;

IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

DECLARE @Id UNIQUEIDENTIFIER

SELECT @Id = INSERTED.Id
FROM INSERTED

UPDATE dbo.Batch
SET CreateDate = GETUTCDATE(), UpdateDate = GETUTCDATE()
WHERE Id = @Id
END   

                "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP TRIGGER [dbo].[BatchLogEntry_UPDATE]

GO

DROP TRIGGER [dbo].[Batch_UPDATE]

GO

DROP TRIGGER [dbo].[BatchLogEntry_INSERT]

GO

DROP TRIGGER [dbo].[Batch_INSERT]
            ");
        }
    }
}
