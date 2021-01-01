using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class BillMaster_BillDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "po",
                table: "BillMaster",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "po",
                table: "BillMaster",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DelieveryPlace",
                schema: "po",
                table: "BillMaster",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "po",
                table: "BillMaster",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentTerm",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickUpDel",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonName",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendorName",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                schema: "po",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesPerson",
                schema: "po");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "DelieveryPlace",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "PaymentTerm",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "PickUpDel",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "SalesPersonName",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "VendorName",
                schema: "po",
                table: "BillMaster");
        }
    }
}
