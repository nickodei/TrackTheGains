using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackTheGains.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_excercises_workouts_WorkoutId",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "fitness",
                table: "workouts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "fitness",
                table: "workouts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "fitness",
                table: "workouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "fitness",
                table: "workouts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "fitness",
                table: "workouts",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutId",
                schema: "fitness",
                table: "excercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "fitness",
                table: "excercises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "fitness",
                table: "excercises",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "fitness",
                table: "excercises",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "fitness",
                table: "excercises",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "fitness",
                table: "excercises",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_excercises_workouts_WorkoutId",
                schema: "fitness",
                table: "excercises",
                column: "WorkoutId",
                principalSchema: "fitness",
                principalTable: "workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_excercises_workouts_WorkoutId",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "fitness",
                table: "workouts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "fitness",
                table: "workouts");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "fitness",
                table: "workouts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "fitness",
                table: "workouts");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "fitness",
                table: "excercises");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "fitness",
                table: "workouts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutId",
                schema: "fitness",
                table: "excercises",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "fitness",
                table: "excercises",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_excercises_workouts_WorkoutId",
                schema: "fitness",
                table: "excercises",
                column: "WorkoutId",
                principalSchema: "fitness",
                principalTable: "workouts",
                principalColumn: "Id");
        }
    }
}
