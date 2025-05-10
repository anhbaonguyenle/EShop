using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerUserName = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    CustomerPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CustomerFullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustomerImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerPhone = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerUserName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
