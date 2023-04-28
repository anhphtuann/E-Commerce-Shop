using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Shop.Migrations
{
    /// <inheritdoc />
    public partial class table5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserProduct");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "UserProduct");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "UserProduct");

            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "UserProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "UserProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "UserProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "UserProduct",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
