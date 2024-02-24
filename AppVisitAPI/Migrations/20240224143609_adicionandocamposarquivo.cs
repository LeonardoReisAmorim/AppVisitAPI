using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVisitAPI.Migrations
{
    /// <inheritdoc />
    public partial class adicionandocamposarquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePlace",
                table: "Arquivos",
                newName: "ArquivoConteudo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Arquivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "Arquivos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "Arquivos");

            migrationBuilder.RenameColumn(
                name: "ArquivoConteudo",
                table: "Arquivos",
                newName: "FilePlace");
        }
    }
}
