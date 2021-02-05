using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class Changedatatypeofgstscheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Scheme1",
                schema: "po",
                table: "BillItem",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Scheme1",
                schema: "po",
                table: "BillItem",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
