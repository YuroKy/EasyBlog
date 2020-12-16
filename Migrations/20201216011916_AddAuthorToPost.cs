using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class AddAuthorToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author_name",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                schema: "easy_blog",
                table: "posts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_posts_author_id",
                schema: "easy_blog",
                table: "posts",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_users_author_id",
                schema: "easy_blog",
                table: "posts",
                column: "author_id",
                principalSchema: "easy_blog",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_users_author_id",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "ix_posts_author_id",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "author_id",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "author_name",
                schema: "easy_blog",
                table: "posts",
                type: "text",
                nullable: true);
        }
    }
}
