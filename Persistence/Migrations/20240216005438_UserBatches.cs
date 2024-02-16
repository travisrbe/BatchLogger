using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserBatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_User_UserId",
                table: "Batch");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Batch",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Batch_UserId",
                table: "Batch",
                newName: "IX_Batch_CreatorUserId");

            migrationBuilder.CreateTable(
                name: "UserBatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBatch_Batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBatch_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBatch_BatchId",
                table: "UserBatch",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBatch_UserId",
                table: "UserBatch",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_User_CreatorUserId",
                table: "Batch",
                column: "CreatorUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_User_CreatorUserId",
                table: "Batch");

            migrationBuilder.DropTable(
                name: "UserBatch");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Batch",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Batch_CreatorUserId",
                table: "Batch",
                newName: "IX_Batch_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_User_UserId",
                table: "Batch",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
