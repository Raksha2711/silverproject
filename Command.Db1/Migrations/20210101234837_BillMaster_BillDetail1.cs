using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class BillMaster_BillDetail1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillDetails",
                schema: "po",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Unit = table.Column<int>(maxLength: 3, nullable: false),
                    BasicRate = table.Column<double>(nullable: false),
                    AddCost = table.Column<double>(nullable: false),
                    CDC = table.Column<double>(nullable: false),
                    Discount1 = table.Column<double>(nullable: false),
                    Scheme1 = table.Column<double>(nullable: false),
                    Scheme2 = table.Column<double>(nullable: false),
                    GSTRate = table.Column<int>(nullable: false),
                    NLC = table.Column<double>(nullable: false),
                    Remarks = table.Column<int>(maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails",
                schema: "po");
        }
    }
}
