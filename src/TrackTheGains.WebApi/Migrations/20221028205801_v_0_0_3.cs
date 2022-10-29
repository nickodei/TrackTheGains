using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackTheGains.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class v003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "excercises",
                schema: "fitness");

            migrationBuilder.CreateTable(
                name: "exercises",
                schema: "fitness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNr = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exercises_workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "fitness",
                        principalTable: "workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exercises_WorkoutId",
                schema: "fitness",
                table: "exercises",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exercises",
                schema: "fitness");

            migrationBuilder.CreateTable(
                name: "excercises",
                schema: "fitness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OrderNr = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_excercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_excercises_workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "fitness",
                        principalTable: "workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_excercises_WorkoutId",
                schema: "fitness",
                table: "excercises",
                column: "WorkoutId");
        }
    }
}
