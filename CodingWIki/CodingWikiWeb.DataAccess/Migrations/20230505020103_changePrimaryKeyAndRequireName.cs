using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWikiWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changePrimaryKeyAndRequireName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "tbl_Genres",
                newName: "Genre_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbl_Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre_Id",
                table: "tbl_Genres",
                newName: "GenreId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbl_Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
