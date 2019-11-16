using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeComfy.Services.Airplanes.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    AvailableSeats = table.Column<string>(nullable: true),
                    AirplaneStatus = table.Column<int>(nullable: false),
                    FlightsCarriedOut = table.Column<int>(nullable: false),
                    NextFlight = table.Column<DateTime>(nullable: true),
                    FlightEnd = table.Column<DateTime>(nullable: true),
                    IntroductionToTheFleet = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airplanes");
        }
    }
}
