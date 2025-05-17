using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Migrations
{
    /// <inheritdoc />
    public partial class FixBillDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Bill_BillIdNavigationBillId",
                table: "BillDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Product_ProductIDNavigationProductID",
                table: "BillDetail");

            migrationBuilder.DropIndex(
                name: "IX_BillDetail_BillIdNavigationBillId",
                table: "BillDetail");

            migrationBuilder.DropIndex(
                name: "IX_BillDetail_ProductIDNavigationProductID",
                table: "BillDetail");

            migrationBuilder.DropColumn(
                name: "BillIdNavigationBillId",
                table: "BillDetail");

            migrationBuilder.DropColumn(
                name: "ProductIDNavigationProductID",
                table: "BillDetail");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillId",
                table: "BillDetail",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductID",
                table: "BillDetail",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Bill_BillId",
                table: "BillDetail",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Product_ProductID",
                table: "BillDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Bill_BillId",
                table: "BillDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Product_ProductID",
                table: "BillDetail");

            migrationBuilder.DropIndex(
                name: "IX_BillDetail_BillId",
                table: "BillDetail");

            migrationBuilder.DropIndex(
                name: "IX_BillDetail_ProductID",
                table: "BillDetail");

            migrationBuilder.AddColumn<int>(
                name: "BillIdNavigationBillId",
                table: "BillDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductIDNavigationProductID",
                table: "BillDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillIdNavigationBillId",
                table: "BillDetail",
                column: "BillIdNavigationBillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductIDNavigationProductID",
                table: "BillDetail",
                column: "ProductIDNavigationProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Bill_BillIdNavigationBillId",
                table: "BillDetail",
                column: "BillIdNavigationBillId",
                principalTable: "Bill",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Product_ProductIDNavigationProductID",
                table: "BillDetail",
                column: "ProductIDNavigationProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
