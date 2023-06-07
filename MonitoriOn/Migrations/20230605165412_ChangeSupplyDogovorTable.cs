using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class ChangeSupplyDogovorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SupplyDogovors");

            migrationBuilder.DropColumn(
                name: "SupplyDogovorId",
                table: "SupplyDogovorAccounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "SupplyDogovors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplyDogovors_AccountId",
                table: "SupplyDogovors",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors",
                column: "AccountId",
                principalTable: "SupplyDogovorAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors");

            migrationBuilder.DropIndex(
                name: "IX_SupplyDogovors_AccountId",
                table: "SupplyDogovors");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "SupplyDogovors");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SupplyDogovors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SupplyDogovorId",
                table: "SupplyDogovorAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
