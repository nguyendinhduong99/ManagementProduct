using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagementAPI.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProProductTypes",
                table: "ProProductTypes");

            migrationBuilder.RenameTable(
                name: "ProProductTypes",
                newName: "ProductTypes");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "ProductTypes",
                newName: "ProProductTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProProductTypes",
                table: "ProProductTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
