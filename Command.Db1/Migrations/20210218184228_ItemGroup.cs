using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Command.Db1.Migrations
{
    public partial class ItemGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemGroup",
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
                    table.PrimaryKey("PK_ItemGroup", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGroup",
                schema: "po");
        }
    }
}
