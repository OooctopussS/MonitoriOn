using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AddSuplyDogovorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplyDogovorId",
                table: "Monitors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupplyDogovorAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyDogovorId = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyDogovorAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplyDogovors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyDogovors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_SupplyDogovorId",
                table: "Monitors",
                column: "SupplyDogovorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_SupplyDogovors_SupplyDogovorId",
                table: "Monitors",
                column: "SupplyDogovorId",
                principalTable: "SupplyDogovors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_SupplyDogovors_SupplyDogovorId",
                table: "Monitors");

            migrationBuilder.DropTable(
                name: "SupplyDogovorAccounts");

            migrationBuilder.DropTable(
                name: "SupplyDogovors");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_SupplyDogovorId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "SupplyDogovorId",
                table: "Monitors");
        }
    }
}
