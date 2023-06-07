using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AddMonitorOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonitorsOrderId",
                table: "Monitors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MonitorsOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPickUp = table.Column<bool>(type: "bit", nullable: false),
                    IsDelivery = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorsOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_MonitorsOrderId",
                table: "Monitors",
                column: "MonitorsOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_MonitorsOrders_MonitorsOrderId",
                table: "Monitors",
                column: "MonitorsOrderId",
                principalTable: "MonitorsOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_MonitorsOrders_MonitorsOrderId",
                table: "Monitors");

            migrationBuilder.DropTable(
                name: "MonitorsOrders");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_MonitorsOrderId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "MonitorsOrderId",
                table: "Monitors");
        }
    }
}
