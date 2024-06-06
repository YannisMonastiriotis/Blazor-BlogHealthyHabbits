using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabbitsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsEmailConfirmedColUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>  (
                name: "IsEmailConfirmed",
                table: "user_account",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "user_account");
        }
    }
}
