﻿using Microsoft.EntityFrameworkCore;

using SciMaterials.DAL.Resources.Contracts.Entities;

using File = SciMaterials.DAL.Resources.Contracts.Entities.File;

namespace SciMaterials.DAL.Resources.Contexts;

public class SciMaterialsContext : DbContext
{
    public SciMaterialsContext(DbContextOptions<SciMaterialsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;
    public virtual DbSet<ContentType> ContentTypes { get; set; } = null!;
    public virtual DbSet<Resource> Resources{ get; set; } = null!;
    public virtual DbSet<Url> Urls { get; set; } = null!;
    public virtual DbSet<File> Files { get; set; } = null!;
    public virtual DbSet<FileGroup> FileGroups { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<Author> Authors { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Rating> Ratings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>(entity =>
            entity.HasOne(comment => comment.Author)
            .WithMany(author => author.Comments)
            .OnDelete(DeleteBehavior.NoAction)
        );
    }
}