using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duanbaimot_lan2.Migrations
{
    public partial class AddResetTokenToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "AspNetUsers");
        }
    }
}
