using Microsoft.EntityFrameworkCore.Migrations;

namespace SafeDriver.Domain.Migrations
{
    public partial class RenameUserNameToEmailAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "users",
                newName: "email_address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_address",
                table: "users",
                newName: "user_name");
        }
    }
}
