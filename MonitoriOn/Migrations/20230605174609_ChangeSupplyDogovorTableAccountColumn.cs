using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class ChangeSupplyDogovorTableAccountColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "SupplyDogovors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors",
                column: "AccountId",
                principalTable: "SupplyDogovorAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "SupplyDogovors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyDogovors_SupplyDogovorAccounts_AccountId",
                table: "SupplyDogovors",
                column: "AccountId",
                principalTable: "SupplyDogovorAccounts",
                principalColumn: "Id");
        }
    }
}
