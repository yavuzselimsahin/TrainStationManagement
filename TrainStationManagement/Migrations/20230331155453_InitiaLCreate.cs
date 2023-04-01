using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainStationManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitiaLCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Train",
                columns: table => new
                {
                    TrainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureStationId = table.Column<int>(type: "int", nullable: false),
                    ArrivalStationId = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.TrainId);
                });

            migrationBuilder.CreateTable(
                name: "TrainStation",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainStation", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Train");

            migrationBuilder.DropTable(
                name: "TrainStation");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
