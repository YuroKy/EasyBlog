﻿// <auto-generated />
using System;
using EasyBlog.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyBlog.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20201214210306_AddTagAndUser")]
    partial class AddTagAndUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("easy_blog")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EasyBlog.Persistence.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .HasColumnName("author_name")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnName("title")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_posts");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("EasyBlog.Persistence.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<Guid?>("PostId")
                        .HasColumnName("post_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.HasIndex("PostId")
                        .HasName("ix_tags_post_id");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("EasyBlog.Persistence.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationTime")
                        .HasColumnName("registration_time")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("EasyBlog.Persistence.Entities.Tag", b =>
                {
                    b.HasOne("EasyBlog.Persistence.Entities.Post", null)
                        .WithMany("Tags")
                        .HasForeignKey("PostId")
                        .HasConstraintName("fk_tags_posts_post_id");
                });
#pragma warning restore 612, 618
        }
    }
}
