using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    cateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.cateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    proId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    cateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.proId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_cateId",
                        column: x => x.cateId,
                        principalTable: "Categories",
                        principalColumn: "cateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    proId = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<double>(type: "float", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_proId",
                        column: x => x.proId,
                        principalTable: "Products",
                        principalColumn: "proId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    cateId = table.Column<int>(type: "int", nullable: false),
                    proId = table.Column<int>(type: "int", nullable: false),
                    priceUnit = table.Column<decimal>(type: "money", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => new { x.cateId, x.proId });
                    table.ForeignKey(
                        name: "FK_Stocks_Categories_cateId",
                        column: x => x.cateId,
                        principalTable: "Categories",
                        principalColumn: "cateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_proId",
                        column: x => x.proId,
                        principalTable: "Products",
                        principalColumn: "proId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_cateId",
                table: "Products",
                column: "cateId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_proId",
                table: "Reviews",
                column: "proId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_proId",
                table: "Stocks",
                column: "proId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
