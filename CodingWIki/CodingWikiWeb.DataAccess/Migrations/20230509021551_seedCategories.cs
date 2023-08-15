using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWikiWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(Name) VALUES('Cat 1')");
            migrationBuilder.Sql("INSERT INTO Categories(Name) VALUES('Cat 2')");
            migrationBuilder.Sql("INSERT INTO Categories(Name) VALUES('Cat 3')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
