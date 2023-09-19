using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modify_start_end_event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Event",
                newName: "StartIn");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Event",
                newName: "EndIn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartIn",
                table: "Event",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndIn",
                table: "Event",
                newName: "End");
        }
    }
}
