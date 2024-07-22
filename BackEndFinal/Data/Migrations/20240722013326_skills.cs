using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class skills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Design",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Development",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Innovation",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamLeader",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Communication",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "Design",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "Development",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "Innovation",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "TeamLeader",
                table: "teachers");
        }
    }
}
