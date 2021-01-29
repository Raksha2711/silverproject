using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class addinbillmaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails",
                schema: "po");

            migrationBuilder.AlterColumn<string>(
                name: "PickUpDel",
                schema: "po",
                table: "BillMaster",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentTerm",
                schema: "po",
                table: "BillMaster",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "AddCost",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BasicRate",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CDC",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DelieveryPlaceId",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Discount1",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "GSTRate",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "NLC",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "POId",
                schema: "po",
                table: "BillMaster",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentValue",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "po",
                table: "BillMaster",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Scheme1",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Scheme2",
                schema: "po",
                table: "BillMaster",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "po",
                table: "BillMaster",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddCost",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "BasicRate",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "CDC",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "DelieveryPlaceId",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Discount1",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "GSTRate",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "NLC",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "POId",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "PaymentValue",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Qty",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Scheme1",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Scheme2",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "po",
                table: "BillMaster");

            migrationBuilder.AlterColumn<int>(
                name: "PickUpDel",
                schema: "po",
                table: "BillMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTerm",
                schema: "po",
                table: "BillMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BillDetails",
                schema: "po",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddCost = table.Column<double>(type: "float", nullable: false),
                    BasicRate = table.Column<double>(type: "float", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    CDC = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Discount1 = table.Column<double>(type: "float", nullable: false),
                    GSTRate = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    NLC = table.Column<double>(type: "float", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    Scheme1 = table.Column<double>(type: "float", nullable: false),
                    Scheme2 = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                });
        }
    }
}
