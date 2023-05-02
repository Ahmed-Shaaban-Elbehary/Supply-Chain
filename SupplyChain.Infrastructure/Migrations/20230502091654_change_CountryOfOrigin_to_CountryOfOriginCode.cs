using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_CountryOfOrigin_to_CountryOfOriginCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOriginCode",
                table: "Products",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryOfOriginCode",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
