using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class changeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_productId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_userId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Review",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_userId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_productId",
                table: "Review",
                newName: "IX_Review_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_UserId",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Review",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Review",
                newName: "IX_Review_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                newName: "IX_Review_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_productId",
                table: "Review",
                column: "productId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_userId",
                table: "Review",
                column: "userId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
