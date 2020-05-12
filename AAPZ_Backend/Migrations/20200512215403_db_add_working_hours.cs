using Microsoft.EntityFrameworkCore.Migrations;

namespace AAPZ_Backend.Migrations
{
    public partial class db_add_working_hours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinishMinute",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinistHour",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartMinute",
                table: "Building",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishMinute",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "FinistHour",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "Building");
        }
    }
}
