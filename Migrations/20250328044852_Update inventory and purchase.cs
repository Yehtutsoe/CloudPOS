using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudPOS.Migrations
{
    /// <inheritdoc />
    public partial class Updateinventoryandpurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AdjustmentDate",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Inventory");

            migrationBuilder.AddColumn<DateTime>(
                name: "EarliestDate",
                table: "StockLedger",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EarliestDate",
                table: "PurchaseDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EarliestDate",
                table: "Inventory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EarliestDate",
                table: "StockLedger");

            migrationBuilder.DropColumn(
                name: "EarliestDate",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "EarliestDate",
                table: "Inventory");

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdjustmentDate",
                table: "Inventory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Inventory",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: true);
        }
    }
}
