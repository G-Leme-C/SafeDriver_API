using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeDriver.Domain.Migrations
{
    public partial class AddPhoneNumberToDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "drivers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "drivers");
        }
    }
}
