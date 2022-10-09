﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SciMaterials.DAL.Contexts;

#nullable disable

namespace SciMaterials.Data.MySqlMigrations.Migrations
{
    [DbContext(typeof(SciMaterialsContext))]
    partial class SciMaterialsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CategoryFile", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FilesId")
                        .HasColumnType("char(36)");

                    b.HasKey("CategoriesId", "FilesId");

                    b.HasIndex("FilesId");

                    b.ToTable("CategoryFile");
                });

            modelBuilder.Entity("CategoryFileGroup", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FileGroupsId")
                        .HasColumnType("char(36)");

                    b.HasKey("CategoriesId", "FileGroupsId");

                    b.HasIndex("FileGroupsId");

                    b.ToTable("CategoryFileGroup");
                });

            modelBuilder.Entity("FileGroupTag", b =>
                {
                    b.Property<Guid>("FileGroupsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("char(36)");

                    b.HasKey("FileGroupsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("FileGroupTag");
                });

            modelBuilder.Entity("FileTag", b =>
                {
                    b.Property<Guid>("FilesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("char(36)");

                    b.HasKey("FilesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("FileTag");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FileGroupId");

                    b.HasIndex("FileId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.ContentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ContentTypes");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ContentTypeId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ContentTypeId");

                    b.HasIndex("FileGroupId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("FileGroups");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FileGroupId");

                    b.HasIndex("FileId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryFile", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.File", null)
                        .WithMany()
                        .HasForeignKey("FilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryFileGroup", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", null)
                        .WithMany()
                        .HasForeignKey("FileGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FileGroupTag", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.FileGroup", null)
                        .WithMany()
                        .HasForeignKey("FileGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FileTag", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.File", null)
                        .WithMany()
                        .HasForeignKey("FilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Comment", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", "FileGroup")
                        .WithMany("Comments")
                        .HasForeignKey("FileGroupId");

                    b.HasOne("SciMaterials.DAL.Models.File", "File")
                        .WithMany("Comments")
                        .HasForeignKey("FileId");

                    b.Navigation("Author");

                    b.Navigation("File");

                    b.Navigation("FileGroup");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "Author")
                        .WithMany("Files")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.ContentType", "ContentType")
                        .WithMany("Files")
                        .HasForeignKey("ContentTypeId");

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", "FileGroup")
                        .WithMany("Files")
                        .HasForeignKey("FileGroupId");

                    b.Navigation("Author");

                    b.Navigation("ContentType");

                    b.Navigation("FileGroup");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Rating", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", "FileGroup")
                        .WithMany("Ratings")
                        .HasForeignKey("FileGroupId");

                    b.HasOne("SciMaterials.DAL.Models.File", "File")
                        .WithMany("Ratings")
                        .HasForeignKey("FileId");

                    b.Navigation("File");

                    b.Navigation("FileGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.ContentType", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
