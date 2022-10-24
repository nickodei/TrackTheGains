using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackTheGains.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNr",
                schema: "fitness",
                table: "excercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNr",
                schema: "fitness",
                table: "excercises");
        }
    }
}
