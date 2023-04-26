using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class tableup16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ForeignId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product",
                column: "ForeignId",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ForeignId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ForeignId",
                table: "Product",
                column: "ForeignId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
