using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Migrations
{
    /// <inheritdoc />
    public partial class FixBillCustomerNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Customer_CustomerUserNameNavigationCustomerUserName",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_CustomerUserNameNavigationCustomerUserName",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "CustomerUserNameNavigationCustomerUserName",
                table: "Bill");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerUserName",
                table: "Bill",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerUserName",
                table: "Bill",
                column: "CustomerUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Customer_CustomerUserName",
                table: "Bill",
                column: "CustomerUserName",
                principalTable: "Customer",
                principalColumn: "CustomerUserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Customer_CustomerUserName",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_CustomerUserName",
                table: "Bill");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerUserName",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.AddColumn<string>(
                name: "CustomerUserNameNavigationCustomerUserName",
                table: "Bill",
                type: "nvarchar(18)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerUserNameNavigationCustomerUserName",
                table: "Bill",
                column: "CustomerUserNameNavigationCustomerUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Customer_CustomerUserNameNavigationCustomerUserName",
                table: "Bill",
                column: "CustomerUserNameNavigationCustomerUserName",
                principalTable: "Customer",
                principalColumn: "CustomerUserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
