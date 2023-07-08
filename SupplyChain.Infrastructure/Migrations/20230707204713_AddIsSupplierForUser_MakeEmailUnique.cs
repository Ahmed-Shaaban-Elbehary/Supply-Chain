using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSupplierForUser_MakeEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSupplier",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsSupplier",
                table: "User");
        }
    }
}
