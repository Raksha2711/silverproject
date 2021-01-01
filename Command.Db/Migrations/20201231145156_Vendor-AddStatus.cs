using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db.Migrations
{
    public partial class VendorAddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "po",
                table: "Vendor",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "po",
                table: "Vendor");
        }
    }
}
