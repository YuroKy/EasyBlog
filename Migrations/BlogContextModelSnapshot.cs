﻿// <auto-generated />
using System;
using EasyBlog.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyBlog.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class BlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("AuthorId")
                        .HasColumnName("author_id")
                        .HasColumnType("uuid");

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

                    b.HasIndex("AuthorId")
                        .HasName("ix_posts_author_id");

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

                    b.Property<string>("Avatar")
                        .HasColumnName("avatar")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.Property<byte[]>("Password")
                        .HasColumnName("password")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnName("registration_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("Salt")
                        .HasColumnName("salt")
                        .HasColumnType("bytea");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("EasyBlog.Persistence.Entities.Post", b =>
                {
                    b.HasOne("EasyBlog.Persistence.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("fk_posts_users_author_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
