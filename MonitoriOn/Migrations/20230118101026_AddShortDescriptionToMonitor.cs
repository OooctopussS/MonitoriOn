using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoriOn.Migrations
{
    public partial class AddShortDescriptionToMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "Monitors");
        }
    }
}
