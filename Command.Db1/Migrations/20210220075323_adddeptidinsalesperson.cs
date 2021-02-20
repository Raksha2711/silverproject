using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class adddeptidinsalesperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptId",
                schema: "po",
                table: "SalesPerson",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeptId1",
                schema: "po",
                table: "SalesPerson",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_DeptId1",
                schema: "po",
                table: "SalesPerson",
                column: "DeptId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPerson_Department_DeptId1",
                schema: "po",
                table: "SalesPerson",
                column: "DeptId1",
                principalSchema: "po",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPerson_Department_DeptId1",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropIndex(
                name: "IX_SalesPerson_DeptId1",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropColumn(
                name: "DeptId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropColumn(
                name: "DeptId1",
                schema: "po",
                table: "SalesPerson");
        }
    }
}
