using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales_Organizer.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders");

            migrationBuilder.DropColumn(
                name: "ProductInOrderId",
                table: "ProductInOrders");

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderId",
                table: "ProductInOrders",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders",
                column: "ProductOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders");

            migrationBuilder.DropColumn(
                name: "ProductOrderId",
                table: "ProductInOrders");

            migrationBuilder.AddColumn<int>(
                name: "ProductInOrderId",
                table: "ProductInOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders",
                column: "ProductInOrderId");
        }
    }
}
