using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class tableup5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryIdCateId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryIdCateId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryIdCateId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ForeignKeyCategoryId",
                table: "Product",
                newName: "CategoryForeignId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryForeignId",
                table: "Product",
                column: "CategoryForeignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryForeignId",
                table: "Product",
                column: "CategoryForeignId",
                principalTable: "Category",
                principalColumn: "CateId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryForeignId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryForeignId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CategoryForeignId",
                table: "Product",
                newName: "ForeignKeyCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryIdCateId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryIdCateId",
                table: "Product",
                column: "CategoryIdCateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryIdCateId",
                table: "Product",
                column: "CategoryIdCateId",
                principalTable: "Category",
                principalColumn: "CateId");
        }
    }
}
