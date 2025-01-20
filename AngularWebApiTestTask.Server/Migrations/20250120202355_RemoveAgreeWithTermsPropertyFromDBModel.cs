using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularWebApiTestTask.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAgreeWithTermsPropertyFromDBModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreeToTerms",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgreeToTerms",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
