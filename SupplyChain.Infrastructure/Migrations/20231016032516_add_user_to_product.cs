using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_to_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplyId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplyId",
                table: "Products",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SupplyId",
                table: "Products",
                column: "SupplyId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SupplyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplyId",
                table: "Products");
        }
    }
}
