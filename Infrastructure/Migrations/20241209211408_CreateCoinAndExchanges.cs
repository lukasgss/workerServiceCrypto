using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateCoinAndExchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RankByMarketCap = table.Column<int>(type: "int", nullable: false),
                    PercentageChangeInOneHour = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MarketCap_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketCap_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TradingVolume_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TradingVolume_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivePairs = table.Column<int>(type: "int", nullable: false),
                    Volume_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_Symbol",
                table: "Coins",
                column: "Symbol");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_Name",
                table: "Exchanges",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
