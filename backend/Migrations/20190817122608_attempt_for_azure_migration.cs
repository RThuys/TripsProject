using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class attempt_for_azure_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripChildren_Children_ChildId",
                table: "TripChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_TripChildren_Trips_TripId",
                table: "TripChildren");

            migrationBuilder.DropIndex(
                name: "IX_TripChildren_ChildId",
                table: "TripChildren");

            migrationBuilder.DropIndex(
                name: "IX_TripChildren_TripId",
                table: "TripChildren");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TripChildren_ChildId",
                table: "TripChildren",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_TripChildren_TripId",
                table: "TripChildren",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripChildren_Children_ChildId",
                table: "TripChildren",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripChildren_Trips_TripId",
                table: "TripChildren",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
