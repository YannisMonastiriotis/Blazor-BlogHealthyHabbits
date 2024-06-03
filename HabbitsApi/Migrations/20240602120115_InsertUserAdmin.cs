using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabbitsApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "user_account",
              columns: new[] { "user_name", "passwprd", "role" },
              values: new object[] { "admin", "Eleni123!", "admin" }
          );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
              table: "user_account",
              keyColumn: "user_name",
              keyValue: "admin"
          );
        }
    }
}
