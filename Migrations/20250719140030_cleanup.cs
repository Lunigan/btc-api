using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Btc.Api.Migrations
{
    /// <inheritdoc />
    public partial class cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyChangePercent",
                table: "BitcoinRates");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "CnbRates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "CnbRates");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyChangePercent",
                table: "BitcoinRates",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: true);
        }
    }
}
