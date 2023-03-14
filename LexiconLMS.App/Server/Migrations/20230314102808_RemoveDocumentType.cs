using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiconLMS.App.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropIndex(
                name: "IX_Document_DocumentTypeId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CMAID",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DocumentFile",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "Document",
                newName: "ErrorCode");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Uploaded",
                table: "Document",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Document_ActivityId",
                table: "Document",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CourseId",
                table: "Document",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ModuleId",
                table: "Document",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Activity_ActivityId",
                table: "Document",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Course_CourseId",
                table: "Document",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Module_ModuleId",
                table: "Document",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Activity_ActivityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Course_CourseId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Module_ModuleId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_ActivityId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_CourseId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_ModuleId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Uploaded",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "ErrorCode",
                table: "Document",
                newName: "DocumentTypeId");

            migrationBuilder.AddColumn<int>(
                name: "CMAID",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentFile",
                table: "Document",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                table: "Document",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
