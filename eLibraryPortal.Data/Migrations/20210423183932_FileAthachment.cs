using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLibraryPortal.Data.Migrations
{
    public partial class FileAthachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAthachmentName",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileAthachment",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAthachment",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "FileAthachmentName",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
