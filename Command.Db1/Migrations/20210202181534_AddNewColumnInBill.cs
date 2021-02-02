using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class AddNewColumnInBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accounts",
                schema: "po",
                table: "Bill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Approver",
                schema: "po",
                table: "Bill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Purchase",
                schema: "po",
                table: "Bill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                schema: "po",
                table: "Bill",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PurchaseInvoiceNo",
                schema: "po",
                table: "Bill",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accounts",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "Approver",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "Purchase",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                schema: "po",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "PurchaseInvoiceNo",
                schema: "po",
                table: "Bill");
        }
    }
}
