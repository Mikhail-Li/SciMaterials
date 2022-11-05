﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SciMaterials.DAL.Contexts;

#nullable disable

namespace SciMaterials.MsSqlServerMigrations.Migrations
{
    [DbContext(typeof(SciMaterialsContext))]
    [Migration("20221105073322_UpdatingLinksTable")]
    partial class UpdatingLinksTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Base.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UrlId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FileGroupId");

                    b.HasIndex("FileId");

                    b.HasIndex("UrlId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.ContentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContentTypes");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessCount")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LastAccess")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("SourceAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FileGroupId");

                    b.HasIndex("FileId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.HasBaseType("SciMaterials.DAL.Models.Base.Resource");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContentTypeId");

                    b.HasIndex("FileGroupId");

                    b.HasIndex("TagId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.HasBaseType("SciMaterials.DAL.Models.Base.Resource");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TagId");

                    b.ToTable("FileGroups");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Url", b =>
                {
                    b.HasBaseType("SciMaterials.DAL.Models.Base.Resource");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Base.Resource", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "Author")
                        .WithMany("Resources")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Category", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithMany("Categories")
                        .HasForeignKey("ResourceId");

                    b.Navigation("Parent");
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

                    b.HasOne("SciMaterials.DAL.Models.Url", null)
                        .WithMany("Comments")
                        .HasForeignKey("UrlId");

                    b.Navigation("Author");

                    b.Navigation("File");

                    b.Navigation("FileGroup");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Rating", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Author", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", "FileGroup")
                        .WithMany()
                        .HasForeignKey("FileGroupId");

                    b.HasOne("SciMaterials.DAL.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithMany("Ratings")
                        .HasForeignKey("ResourceId");

                    b.Navigation("File");

                    b.Navigation("FileGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Tag", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithMany("Tags")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Category", null)
                        .WithMany("Files")
                        .HasForeignKey("CategoryId");

                    b.HasOne("SciMaterials.DAL.Models.ContentType", "ContentType")
                        .WithMany("Files")
                        .HasForeignKey("ContentTypeId");

                    b.HasOne("SciMaterials.DAL.Models.FileGroup", "FileGroup")
                        .WithMany("Files")
                        .HasForeignKey("FileGroupId");

                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithOne()
                        .HasForeignKey("SciMaterials.DAL.Models.File", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.Tag", null)
                        .WithMany("Files")
                        .HasForeignKey("TagId");

                    b.Navigation("ContentType");

                    b.Navigation("FileGroup");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Category", null)
                        .WithMany("FileGroups")
                        .HasForeignKey("CategoryId");

                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithOne()
                        .HasForeignKey("SciMaterials.DAL.Models.FileGroup", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SciMaterials.DAL.Models.Tag", null)
                        .WithMany("FileGroups")
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Url", b =>
                {
                    b.HasOne("SciMaterials.DAL.Models.Base.Resource", null)
                        .WithOne()
                        .HasForeignKey("SciMaterials.DAL.Models.Url", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Author", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Base.Resource", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Ratings");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("FileGroups");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.ContentType", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Tag", b =>
                {
                    b.Navigation("FileGroups");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.File", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.FileGroup", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("SciMaterials.DAL.Models.Url", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
