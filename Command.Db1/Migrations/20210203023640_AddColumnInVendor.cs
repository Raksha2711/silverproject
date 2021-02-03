using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class AddColumnInVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "po",
                table: "Vendor",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                schema: "po",
                table: "Vendor",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                schema: "po",
                table: "Vendor",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "po",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                schema: "po",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "EmailId",
                schema: "po",
                table: "Vendor");
        }
    }
}
