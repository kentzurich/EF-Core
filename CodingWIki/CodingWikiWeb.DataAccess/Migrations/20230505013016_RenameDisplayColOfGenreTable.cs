using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWikiWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameDisplayColOfGenreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Display",
                table: "Genres",
                newName: "DisplayOrder");

            //migrationBuilder.Sql("query here");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Genres",
                newName: "Display");
        }
    }
}
