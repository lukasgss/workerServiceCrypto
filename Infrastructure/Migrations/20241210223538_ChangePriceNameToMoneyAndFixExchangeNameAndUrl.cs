using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePriceNameToMoneyAndFixExchangeNameAndUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Volume_Code",
                table: "Exchanges",
                newName: "Volume_Currency");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "Coins",
                newName: "Money_Amount");

            migrationBuilder.RenameColumn(
                name: "TradingVolume_Code",
                table: "Coins",
                newName: "TradingVolume_Currency");

            migrationBuilder.RenameColumn(
                name: "Price_Code",
                table: "Coins",
                newName: "Money_Currency");

            migrationBuilder.RenameColumn(
                name: "MarketCap_Code",
                table: "Coins",
                newName: "MarketCap_Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Exchanges",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Exchanges",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "NameId",
                table: "Exchanges",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Exchanges");

            migrationBuilder.RenameColumn(
                name: "Volume_Currency",
                table: "Exchanges",
                newName: "Volume_Code");

            migrationBuilder.RenameColumn(
                name: "Money_Amount",
                table: "Coins",
                newName: "Price_Amount");

            migrationBuilder.RenameColumn(
                name: "TradingVolume_Currency",
                table: "Coins",
                newName: "TradingVolume_Code");

            migrationBuilder.RenameColumn(
                name: "Money_Currency",
                table: "Coins",
                newName: "Price_Code");

            migrationBuilder.RenameColumn(
                name: "MarketCap_Currency",
                table: "Coins",
                newName: "MarketCap_Code");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Exchanges",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Exchanges",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
