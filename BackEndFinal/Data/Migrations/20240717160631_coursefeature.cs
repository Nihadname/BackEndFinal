using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class coursefeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Starts = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    ClassDuration = table.Column<string>(nullable: true),
                    SkillLevel = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Students = table.Column<int>(nullable: false),
                    Assessments = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courseFeatures_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseFeatures_CourseId",
                table: "courseFeatures",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseFeatures_courses_CourseId",
                table: "courseFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courseFeatures",
                table: "courseFeatures");

            migrationBuilder.RenameTable(
                name: "courseFeatures",
                newName: "CourseFeature");

            migrationBuilder.RenameIndex(
                name: "IX_courseFeatures_CourseId",
                table: "CourseFeature",
                newName: "IX_CourseFeature_CourseId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "courses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFeature",
                table: "CourseFeature",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeature_courses_CourseId",
                table: "CourseFeature",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
