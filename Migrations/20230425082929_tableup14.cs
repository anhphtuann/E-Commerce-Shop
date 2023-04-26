using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class tableup14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryForeignId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CategoryForeignId",
                table: "Product",
                newName: "ForeignId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryForeignId",
                table: "Product",
                newName: "IX_Product_ForeignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product",
                column: "ForeignId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ForeignId",
                table: "Product",
                newName: "CategoryForeignId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ForeignId",
                table: "Product",
                newName: "IX_Product_CategoryForeignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryForeignId",
                table: "Product",
                column: "CategoryForeignId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
