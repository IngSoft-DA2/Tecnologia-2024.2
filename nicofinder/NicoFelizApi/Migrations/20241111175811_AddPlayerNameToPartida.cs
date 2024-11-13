using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NicoFelizApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerNameToPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "Partidas");
        }
    }
}
