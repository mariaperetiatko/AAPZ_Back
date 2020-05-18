using Microsoft.EntityFrameworkCore.Migrations;

namespace AAPZ_Backend.Migrations
{
    public partial class add_sceduler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishMinute",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "FinistHour",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "Building");

            migrationBuilder.AddColumn<double>(
                name: "FriFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FriStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MonFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MonStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SatFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SatStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SunFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SunStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ThuFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ThuStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TueFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TueStartTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WedFinishTime",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WedStartTime",
                table: "Building",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "FriStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "MonFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "MonStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "SatFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "SatStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "SunFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "SunStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "ThuFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "ThuStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "TueFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "TueStartTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "WedFinishTime",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "WedStartTime",
                table: "Building");

            migrationBuilder.AddColumn<int>(
                name: "FinishMinute",
                table: "Building",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinistHour",
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
    }
}
