using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class recstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "auth",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RecStatus",
                schema: "auth",
                table: "AspNetUsers",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecStatus",
                schema: "auth",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                schema: "auth",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
