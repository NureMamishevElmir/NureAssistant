using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeAIFileIntoFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIFileChats_AIFiles_AIFileId",
                table: "AIFileChats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AIFiles_FileId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "AIFiles");

            migrationBuilder.RenameColumn(
                name: "AIFileId",
                table: "AIFileChats",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_AIFileChats_AIFileId",
                table: "AIFileChats",
                newName: "IX_AIFileChats_FileId");

            migrationBuilder.AddColumn<Guid>(
                name: "AIId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AIId",
                table: "Files",
                column: "AIId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIFileChats_Files_FileId",
                table: "AIFileChats",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AIs_AIId",
                table: "Files",
                column: "AIId",
                principalTable: "AIs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Files_FileId",
                table: "Messages",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIFileChats_Files_FileId",
                table: "AIFileChats");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_AIs_AIId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Files_FileId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Files_AIId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AIId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "AIFileChats",
                newName: "AIFileId");

            migrationBuilder.RenameIndex(
                name: "IX_AIFileChats_FileId",
                table: "AIFileChats",
                newName: "IX_AIFileChats_AIFileId");

            migrationBuilder.CreateTable(
                name: "AIFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AIId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIFiles_AIs_AIId",
                        column: x => x.AIId,
                        principalTable: "AIs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AIFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIFiles_AIId",
                table: "AIFiles",
                column: "AIId");

            migrationBuilder.CreateIndex(
                name: "IX_AIFiles_FileId",
                table: "AIFiles",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIFileChats_AIFiles_AIFileId",
                table: "AIFileChats",
                column: "AIFileId",
                principalTable: "AIFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AIFiles_FileId",
                table: "Messages",
                column: "FileId",
                principalTable: "AIFiles",
                principalColumn: "Id");
        }
    }
}
