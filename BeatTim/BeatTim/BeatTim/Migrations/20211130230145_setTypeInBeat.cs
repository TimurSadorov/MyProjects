using Microsoft.EntityFrameworkCore.Migrations;

namespace BeatTim.Migrations
{
    public partial class setTypeInBeat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Beats",
                type: "numeric(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(2,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Beats",
                type: "numeric(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,2)");
        }
    }
}
