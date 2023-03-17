using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiconLMS.App.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Module",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Activity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Activity");
        }
    }
}
