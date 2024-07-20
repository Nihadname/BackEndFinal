using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class links : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceBookUrl",
                table: "teacherContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntaUrl",
                table: "teacherContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SkypeUrl",
                table: "teacherContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pinterestUrl",
                table: "teacherContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceBookUrl",
                table: "teacherContactInfos");

            migrationBuilder.DropColumn(
                name: "IntaUrl",
                table: "teacherContactInfos");

            migrationBuilder.DropColumn(
                name: "SkypeUrl",
                table: "teacherContactInfos");

            migrationBuilder.DropColumn(
                name: "pinterestUrl",
                table: "teacherContactInfos");
        }
    }
}
