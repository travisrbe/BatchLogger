using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BatchCreator_To_BatchOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_User_CreatorUserId",
                table: "Batch");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Batch",
                newName: "OwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Batch_CreatorUserId",
                table: "Batch",
                newName: "IX_Batch_OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_User_OwnerUserId",
                table: "Batch",
                column: "OwnerUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_User_OwnerUserId",
                table: "Batch");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                table: "Batch",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Batch_OwnerUserId",
                table: "Batch",
                newName: "IX_Batch_CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_User_CreatorUserId",
                table: "Batch",
                column: "CreatorUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
