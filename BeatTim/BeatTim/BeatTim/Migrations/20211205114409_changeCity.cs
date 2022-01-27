using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatTim.Migrations
{
    public partial class changeCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Cities_CityId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_CityId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_ClientId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserProfiles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ClientId",
                table: "UserProfiles",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_ClientId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CityId",
                table: "UserProfiles",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ClientId",
                table: "UserProfiles",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Cities_CityId",
                table: "UserProfiles",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
