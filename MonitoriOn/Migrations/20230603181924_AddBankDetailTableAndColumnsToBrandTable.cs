using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AddBankDetailTableAndColumnsToBrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankDetailId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainAccountant",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccount = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INN = table.Column<int>(type: "int", nullable: false),
                    BankBIC = table.Column<int>(type: "int", nullable: false),
                    CorrespondentAccount = table.Column<int>(type: "int", nullable: false),
                    BankAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BankDetailId",
                table: "Brands",
                column: "BankDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_BankDetail_BankDetailId",
                table: "Brands",
                column: "BankDetailId",
                principalTable: "BankDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_BankDetail_BankDetailId",
                table: "Brands");

            migrationBuilder.DropTable(
                name: "BankDetail");

            migrationBuilder.DropIndex(
                name: "IX_Brands_BankDetailId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BankDetailId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "MainAccountant",
                table: "Brands");
        }
    }
}
