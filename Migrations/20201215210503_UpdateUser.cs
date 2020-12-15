using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                schema: "easy_blog",
                table: "users");

            migrationBuilder.AddColumn<byte[]>(
                name: "password",
                schema: "easy_blog",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "salt",
                schema: "easy_blog",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                schema: "easy_blog",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                schema: "easy_blog",
                table: "users");

            migrationBuilder.DropColumn(
                name: "username",
                schema: "easy_blog",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "easy_blog",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
