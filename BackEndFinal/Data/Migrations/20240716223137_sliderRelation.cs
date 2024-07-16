using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class sliderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderContenttId",
                table: "sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "contents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_contents_SliderId",
                table: "contents",
                column: "SliderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_contents_sliders_SliderId",
                table: "contents",
                column: "SliderId",
                principalTable: "sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contents_sliders_SliderId",
                table: "contents");

            migrationBuilder.DropIndex(
                name: "IX_contents_SliderId",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "SliderContenttId",
                table: "sliders");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "contents");
        }
    }
}
