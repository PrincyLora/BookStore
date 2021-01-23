using Microsoft.EntityFrameworkCore.Migrations;

namespace practise.BookStoreApp.Migrations
{
    public partial class adednewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageURL",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageURL",
                table: "Books");
        }
    }
}
