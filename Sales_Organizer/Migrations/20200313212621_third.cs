using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales_Organizer.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInOrders_Orders_OrderId",
                table: "ProductInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInOrders_Products_ProductId",
                table: "ProductInOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders");

            migrationBuilder.RenameTable(
                name: "ProductInOrders",
                newName: "ProductOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInOrders_ProductId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInOrders_OrderId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders",
                column: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders");

            migrationBuilder.RenameTable(
                name: "ProductOrders",
                newName: "ProductInOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductInOrders",
                newName: "IX_ProductInOrders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductInOrders",
                newName: "IX_ProductInOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrders",
                table: "ProductInOrders",
                column: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInOrders_Orders_OrderId",
                table: "ProductInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInOrders_Products_ProductId",
                table: "ProductInOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
