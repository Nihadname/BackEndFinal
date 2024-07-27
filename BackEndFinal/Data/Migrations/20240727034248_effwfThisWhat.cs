using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class effwfThisWhat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speaker_events_EventId",
                table: "Speaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker");

            migrationBuilder.RenameTable(
                name: "Speaker",
                newName: "speakers");

            migrationBuilder.RenameIndex(
                name: "IX_Speaker_EventId",
                table: "speakers",
                newName: "IX_speakers_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_speakers",
                table: "speakers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_speakers_events_EventId",
                table: "speakers",
                column: "EventId",
                principalTable: "events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_speakers_events_EventId",
                table: "speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_speakers",
                table: "speakers");

            migrationBuilder.RenameTable(
                name: "speakers",
                newName: "Speaker");

            migrationBuilder.RenameIndex(
                name: "IX_speakers_EventId",
                table: "Speaker",
                newName: "IX_Speaker_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Speaker_events_EventId",
                table: "Speaker",
                column: "EventId",
                principalTable: "events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
