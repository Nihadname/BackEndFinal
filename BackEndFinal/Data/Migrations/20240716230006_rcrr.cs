using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class rcrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "contents");

            migrationBuilder.RenameColumn(
                name: "SliderContenttId",
                table: "sliders",
                newName: "SliderContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SliderContentId",
                table: "sliders",
                newName: "SliderContenttId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "contents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
