using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeDriver.Domain.Migrations
{
    public partial class AddDriverScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "drivers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "score",
                table: "drivers");
        }
    }
}
