using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVisitAPI.Migrations
{
    /// <inheritdoc />
    public partial class blobarquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "Arquivos",
                type: "LongBlob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "Arquivos",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "LongBlob");
        }
    }
}
