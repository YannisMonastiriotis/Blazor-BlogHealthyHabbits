using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabbitsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddColEmailUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user_account",
                type: "TEXT",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "user_account");
        }
    }
}
