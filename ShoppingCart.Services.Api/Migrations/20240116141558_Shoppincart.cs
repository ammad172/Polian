using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class Shoppincart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoppingCartHeaders",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCartHeaders", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateTable(
                name: "shoppingCartDetails",
                columns: table => new
                {
                    CardDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCartDetails", x => x.CardDetailId);
                    table.ForeignKey(
                        name: "FK_shoppingCartDetails_shoppingCartHeaders_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "shoppingCartHeaders",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartDetails_ShoppingCartId",
                table: "shoppingCartDetails",
                column: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCartDetails");

            migrationBuilder.DropTable(
                name: "shoppingCartHeaders");
        }
    }
}
