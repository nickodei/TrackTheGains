using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackTheGains.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNr",
                schema: "fitness",
                table: "exercises",
                newName: "Order");

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSessions_workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "fitness",
                        principalTable: "workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repetitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    WeightsInKg = table.Column<int>(type: "integer", nullable: false),
                    SetId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repetitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repetitions_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinishedExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutSessionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedExercises_WorkoutSessions_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinishedExercises_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "fitness",
                        principalTable: "exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedExercises_ExerciseId",
                table: "FinishedExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedExercises_WorkoutSessionId",
                table: "FinishedExercises",
                column: "WorkoutSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Repetitions_SetId",
                table: "Repetitions",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_WorkoutId",
                table: "WorkoutSessions",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishedExercises");

            migrationBuilder.DropTable(
                name: "Repetitions");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.RenameColumn(
                name: "Order",
                schema: "fitness",
                table: "exercises",
                newName: "OrderNr");
        }
    }
}
