using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeDriver.Domain.Migrations
{
    public partial class TripStartTimestampColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "trip_start_timestamp",
                table: "trips",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trip_start_timestamp",
                table: "trips");
        }
    }
}
