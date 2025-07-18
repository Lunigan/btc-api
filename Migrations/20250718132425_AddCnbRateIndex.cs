using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Btc.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCnbRateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "CnbRates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CnbRates_CurrencyCode_ValidFor",
                table: "CnbRates",
                columns: new[] { "CurrencyCode", "ValidFor" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CnbRates_CurrencyCode_ValidFor",
                table: "CnbRates");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "CnbRates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
