using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class Productupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocalPath",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 1, 28, 14, 48, 35, 791, DateTimeKind.Local).AddTicks(925), null });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 1, 28, 14, 48, 35, 791, DateTimeKind.Local).AddTicks(952), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocalPath",
                table: "products");

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4294));

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4320));
        }
    }
}
