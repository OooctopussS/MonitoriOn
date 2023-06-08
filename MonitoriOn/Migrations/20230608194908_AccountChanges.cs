using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AccountChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SupplyDogovorAccounts",
                newName: "DateSold");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRecieved",
                table: "SupplyDogovorAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRecieved",
                table: "SupplyDogovorAccounts");

            migrationBuilder.RenameColumn(
                name: "DateSold",
                table: "SupplyDogovorAccounts",
                newName: "Date");
        }
    }
}
