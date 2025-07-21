using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Btc.Api.Migrations
{
    /// <inheritdoc />
    public partial class modelsPolished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CnbRates");

            migrationBuilder.AddColumn<string>(
                name: "Instrument",
                table: "BitcoinRates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceCurrencyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ValidFor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_SourceCurrencyCode_ValidFor",
                table: "CurrencyRates",
                columns: new[] { "SourceCurrencyCode", "ValidFor" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropColumn(
                name: "Instrument",
                table: "BitcoinRates");

            migrationBuilder.CreateTable(
                name: "CnbRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidFor = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CnbRates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CnbRates_CurrencyCode_ValidFor",
                table: "CnbRates",
                columns: new[] { "CurrencyCode", "ValidFor" },
                unique: true);
        }
    }
}
