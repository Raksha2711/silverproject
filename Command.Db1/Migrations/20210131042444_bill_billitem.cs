using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class bill_billitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                schema: "po",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalesPerson = table.Column<string>(nullable: true),
                    Vendor = table.Column<int>(nullable: false),
                    DeliveryType = table.Column<string>(nullable: true),
                    DelieveryPlace = table.Column<string>(maxLength: 100, nullable: true),
                    DelieveryPlaceId = table.Column<int>(nullable: true),
                    PaymentTerm = table.Column<string>(nullable: true),
                    PaymentValue = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Recstatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillItem",
                schema: "po",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: true),
                    Unit = table.Column<string>(maxLength: 3, nullable: true),
                    BasicRate = table.Column<double>(nullable: false),
                    AddCost = table.Column<double>(nullable: false),
                    CDC = table.Column<double>(nullable: false),
                    Discount1 = table.Column<double>(nullable: false),
                    Scheme1 = table.Column<double>(nullable: false),
                    Scheme2 = table.Column<double>(nullable: false),
                    SchemeAmt = table.Column<double>(nullable: false),
                    GSTRate = table.Column<int>(nullable: false),
                    NLC = table.Column<double>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 150, nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Recstatus = table.Column<string>(nullable: true),
                    BillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItem_Bill_BillId",
                        column: x => x.BillId,
                        principalSchema: "po",
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_BillId",
                schema: "po",
                table: "BillItem",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItem",
                schema: "po");

            migrationBuilder.DropTable(
                name: "Bill",
                schema: "po");
        }
    }
}
