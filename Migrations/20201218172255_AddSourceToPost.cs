using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class AddSourceToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "source_id",
                schema: "easy_blog",
                table: "posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_posts_source_id",
                schema: "easy_blog",
                table: "posts",
                column: "source_id");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_sources_source_id",
                schema: "easy_blog",
                table: "posts",
                column: "source_id",
                principalSchema: "easy_blog",
                principalTable: "sources",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_sources_source_id",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "ix_posts_source_id",
                schema: "easy_blog",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "source_id",
                schema: "easy_blog",
                table: "posts");
        }
    }
}
