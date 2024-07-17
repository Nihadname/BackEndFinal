using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "eventImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "eventImages");
        }
    }
}
