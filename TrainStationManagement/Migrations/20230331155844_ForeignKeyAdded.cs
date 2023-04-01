using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainStationManagement.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Train_ArrivalStationId",
                table: "Train",
                column: "ArrivalStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Train_DepartureStationId",
                table: "Train",
                column: "DepartureStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Train_TrainStation_ArrivalStationId",
                table: "Train",
                column: "ArrivalStationId",
                principalTable: "TrainStation",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Train_TrainStation_DepartureStationId",
                table: "Train",
                column: "DepartureStationId",
                principalTable: "TrainStation",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Train_TrainStation_ArrivalStationId",
                table: "Train");

            migrationBuilder.DropForeignKey(
                name: "FK_Train_TrainStation_DepartureStationId",
                table: "Train");

            migrationBuilder.DropIndex(
                name: "IX_Train_ArrivalStationId",
                table: "Train");

            migrationBuilder.DropIndex(
                name: "IX_Train_DepartureStationId",
                table: "Train");
        }
    }
}
