using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Soefwfe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                table: "teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hobbies",
                table: "teachers");
        }
    }
}
