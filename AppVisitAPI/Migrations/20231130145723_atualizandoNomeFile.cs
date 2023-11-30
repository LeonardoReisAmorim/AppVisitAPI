using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVisitAPI.Migrations
{
    /// <inheritdoc />
    public partial class atualizandoNomeFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Arquivos",
                newName: "FilePlace");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePlace",
                table: "Arquivos",
                newName: "File");
        }
    }
}
