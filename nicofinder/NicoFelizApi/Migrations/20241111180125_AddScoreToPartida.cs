using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NicoFelizApi.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreToPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Partidas");
        }
    }
}
