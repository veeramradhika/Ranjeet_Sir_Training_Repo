using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.API.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zoner",
                table: "Book",
                newName: "BookZoner");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Book",
                newName: "BookName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Book",
                newName: "BookId");

            migrationBuilder.AddColumn<string>(
                name: "BookImage",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BookZoner",
                table: "Book",
                newName: "Zoner");

            migrationBuilder.RenameColumn(
                name: "BookName",
                table: "Book",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Book",
                newName: "Id");
        }
    }
}
