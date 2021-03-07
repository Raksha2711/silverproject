using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class ReasonMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                schema: "po",
                table: "SalesPerson",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reason",
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
                    table.PrimaryKey("PK_Reason", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_ReasonId",
                schema: "po",
                table: "SalesPerson",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPerson_Reason_ReasonId",
                schema: "po",
                table: "SalesPerson",
                column: "ReasonId",
                principalSchema: "po",
                principalTable: "Reason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPerson_Reason_ReasonId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropTable(
                name: "Reason",
                schema: "po");

            migrationBuilder.DropIndex(
                name: "IX_SalesPerson_ReasonId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                schema: "po",
                table: "SalesPerson");
        }
    }
}
