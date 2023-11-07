using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_event_reference_from_ProductQuantityRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantityRequests_Events_EventId",
                table: "ProductQuantityRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProductQuantityRequests_EventId",
                table: "ProductQuantityRequests");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ProductQuantityRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "ProductQuantityRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantityRequests_EventId",
                table: "ProductQuantityRequests",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantityRequests_Events_EventId",
                table: "ProductQuantityRequests",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
