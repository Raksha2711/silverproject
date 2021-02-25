using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class addItemGroupModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "po",
                table: "ItemGroup");

            migrationBuilder.AddColumn<string>(
                name: "ItemGroupNLevelString",
                schema: "po",
                table: "ItemGroup",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemGroupName",
                schema: "po",
                table: "ItemGroup",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentItemGroupId",
                schema: "po",
                table: "ItemGroup",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemGroupNLevelString",
                schema: "po",
                table: "ItemGroup");

            migrationBuilder.DropColumn(
                name: "ItemGroupName",
                schema: "po",
                table: "ItemGroup");

            migrationBuilder.DropColumn(
                name: "ParentItemGroupId",
                schema: "po",
                table: "ItemGroup");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "po",
                table: "ItemGroup",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
