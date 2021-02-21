using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class additemgroupid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemGroupId",
                schema: "po",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemGroupId",
                schema: "po",
                table: "Item",
                column: "ItemGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                schema: "po",
                table: "Item",
                column: "ItemGroupId",
                principalSchema: "po",
                principalTable: "ItemGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                schema: "po",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                schema: "po",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                schema: "po",
                table: "Item");
        }
    }
}
