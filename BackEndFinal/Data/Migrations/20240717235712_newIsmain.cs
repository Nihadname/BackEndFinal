using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class newIsmain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "blogs");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "blogImages",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "blogImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "blogs",
                type: "bit",
                nullable: true);
        }
    }
}
