using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Product.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryName", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Category 1", new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4294), "Description for Product 1", "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fproducts&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAJ", "Product 1", 19.99m, null },
                    { 2, "Category 2", new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4320), "Description for Product 2", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pexels.com%2Fsearch%2Fproduct%2F&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAE", "Product 2", 29.99m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
