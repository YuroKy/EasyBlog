using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBlog.Migrations
{
    public partial class FixUserRegTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registration_time",
                schema: "easy_blog",
                table: "users");

            migrationBuilder.AddColumn<DateTime>(
                name: "registration_time",
                schema: "easy_blog",
                table: "users",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registration_time",
                schema: "easy_blog",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "registration_time",
                schema: "easy_blog",
                table: "users",
                nullable: true);
        }
    }
}
