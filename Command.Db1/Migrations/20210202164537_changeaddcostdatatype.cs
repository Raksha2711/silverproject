using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class changeaddcostdatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelieveryPlace",
                schema: "po",
                table: "Bill");

            migrationBuilder.AlterColumn<string>(
                name: "AddCost",
                schema: "po",
                table: "BillItem",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryType",
                schema: "po",
                table: "Bill",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AddCost",
                schema: "po",
                table: "BillItem",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryType",
                schema: "po",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DelieveryPlace",
                schema: "po",
                table: "Bill",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
