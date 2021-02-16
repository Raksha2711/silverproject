using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class addcolumninPO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                schema: "po",
                table: "Bill",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                schema: "po",
                table: "Bill",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                schema: "po",
                table: "Bill",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNo",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "EmailId",
                schema: "po",
                table: "Bill");
        }
    }
}
