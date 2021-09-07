using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeDriver.Domain.Migrations
{
    public partial class EventDriverReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "driver_id",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_events_driver_id",
                table: "events",
                column: "driver_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_users_driver_id",
                table: "events",
                column: "driver_id",
                principalTable: "drivers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_users_driver_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_driver_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "driver_id",
                table: "events");
        }
    }
}
