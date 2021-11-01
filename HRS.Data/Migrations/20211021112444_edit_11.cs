using Microsoft.EntityFrameworkCore.Migrations;

namespace HRS.Data.Migrations
{
    public partial class edit_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Holidays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
