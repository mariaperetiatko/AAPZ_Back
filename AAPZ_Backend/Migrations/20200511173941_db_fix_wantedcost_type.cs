using Microsoft.EntityFrameworkCore.Migrations;

namespace AAPZ_Backend.Migrations
{
    public partial class db_fix_wantedcost_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WantedCost",
                table: "SearchSetting",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WantedCost",
                table: "SearchSetting",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
