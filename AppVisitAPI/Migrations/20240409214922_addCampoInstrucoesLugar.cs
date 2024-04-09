using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVisitAPI.Migrations
{
    /// <inheritdoc />
    public partial class addCampoInstrucoesLugar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstrucoesUtilizacaoVR",
                table: "Lugares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstrucoesUtilizacaoVR",
                table: "Lugares");
        }
    }
}
