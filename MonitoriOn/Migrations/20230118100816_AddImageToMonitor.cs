using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AddImageToMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Monitors");
        }
    }
}
