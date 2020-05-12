using Microsoft.EntityFrameworkCore.Migrations;

namespace AAPZ_Backend.Migrations
{
    public partial class db_upgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "WorkplaceEquipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Diastance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Client",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Diastance_ClientId",
                table: "Diastance",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diastance_Client_ClientId",
                table: "Diastance",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diastance_Client_ClientId",
                table: "Diastance");

            migrationBuilder.DropIndex(
                name: "IX_Diastance_ClientId",
                table: "Diastance");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "WorkplaceEquipment");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Diastance");

            migrationBuilder.AlterColumn<long>(
                name: "Phone",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
