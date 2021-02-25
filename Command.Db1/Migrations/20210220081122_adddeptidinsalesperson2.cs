using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class adddeptidinsalesperson2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                schema: "po",
                table: "SalesPerson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_DepartmentId",
                schema: "po",
                table: "SalesPerson",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPerson_Department_DepartmentId",
                schema: "po",
                table: "SalesPerson",
                column: "DepartmentId",
                principalSchema: "po",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPerson_Department_DepartmentId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropIndex(
                name: "IX_SalesPerson_DepartmentId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "po",
                table: "SalesPerson");

            migrationBuilder.AddColumn<string>(
                name: "DeptId",
                schema: "po",
                table: "SalesPerson",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeptId1",
                schema: "po",
                table: "SalesPerson",
                type: "int",
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
    }
}
