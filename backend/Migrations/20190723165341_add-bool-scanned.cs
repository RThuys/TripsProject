using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class addboolscanned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Supervisors_SupervisorId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_SupervisorId",
                table: "Trips");

            migrationBuilder.AddColumn<bool>(
                name: "Scanned",
                table: "TripChildren",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scanned",
                table: "TripChildren");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SupervisorId",
                table: "Trips",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Supervisors_SupervisorId",
                table: "Trips",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
