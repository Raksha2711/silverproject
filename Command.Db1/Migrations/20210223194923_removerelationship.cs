using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class removerelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                schema: "po",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                schema: "po",
                table: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
