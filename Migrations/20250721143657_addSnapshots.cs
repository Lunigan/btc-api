using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Btc.Api.Migrations
{
    /// <inheritdoc />
    public partial class addSnapshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BitcoinRateRecordSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BtcEur = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    EurCzk = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinRateRecordSnapshots", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitcoinRateRecordSnapshots");
        }
    }
}
