using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class FixTagTableRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tags_posts_post_id",
                schema: "easy_blog",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "ix_tags_post_id",
                schema: "easy_blog",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "post_id",
                schema: "easy_blog",
                table: "tags");

            migrationBuilder.CreateTable(
                name: "post_tag",
                schema: "easy_blog",
                columns: table => new
                {
                    post_id = table.Column<Guid>(nullable: false),
                    tag_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_tag", x => new { x.post_id, x.tag_id });
                    table.ForeignKey(
                        name: "fk_post_tag_posts_post_id",
                        column: x => x.post_id,
                        principalSchema: "easy_blog",
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_tag_tags_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "easy_blog",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_post_tag_tag_id",
                schema: "easy_blog",
                table: "post_tag",
                column: "tag_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_tag",
                schema: "easy_blog");

            migrationBuilder.AddColumn<Guid>(
                name: "post_id",
                schema: "easy_blog",
                table: "tags",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_tags_post_id",
                schema: "easy_blog",
                table: "tags",
                column: "post_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tags_posts_post_id",
                schema: "easy_blog",
                table: "tags",
                column: "post_id",
                principalSchema: "easy_blog",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
