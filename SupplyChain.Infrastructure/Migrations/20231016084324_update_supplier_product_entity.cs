using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_supplier_product_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SupplyId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SupplyId",
                table: "Products",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplyId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SupplierId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Products",
                newName: "SupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                newName: "IX_Products_SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SupplyId",
                table: "Products",
                column: "SupplyId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
