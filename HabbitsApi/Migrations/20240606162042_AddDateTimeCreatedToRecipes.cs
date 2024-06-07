using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabbitsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeCreatedToRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Recipes",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Recipes");
        }
    }
}
