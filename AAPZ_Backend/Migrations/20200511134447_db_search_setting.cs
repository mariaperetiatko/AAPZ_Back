using Microsoft.EntityFrameworkCore.Migrations;

namespace AAPZ_Backend.Migrations
{
    public partial class db_search_setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Y",
                table: "Building",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "X",
                table: "Building",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SearchSetting",
                columns: table => new
                {
                    SearchSettingId = table.Column<int>(nullable: false),
                    Radius = table.Column<double>(nullable: false),
                    WantedCost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchSetting", x => x.SearchSettingId);
                    table.ForeignKey(
                        name: "FK_SearchSetting_Client_SearchSettingId",
                        column: x => x.SearchSettingId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchSetting");

            migrationBuilder.AlterColumn<int>(
                name: "Y",
                table: "Building",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "X",
                table: "Building",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
