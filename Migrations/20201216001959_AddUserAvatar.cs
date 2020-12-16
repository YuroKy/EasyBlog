using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class AddUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar",
                schema: "easy_blog",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                schema: "easy_blog",
                table: "users");
        }
    }
}
