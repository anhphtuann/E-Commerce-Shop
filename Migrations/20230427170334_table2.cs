using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class table2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_ProductsProductId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductsProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductsProductId",
                table: "Product",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_ProductsProductId",
                table: "Product",
                column: "ProductsProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
