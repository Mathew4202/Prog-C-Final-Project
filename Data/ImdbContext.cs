using System;
using System.Collections.Generic;
using Final_Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Data;

public partial class ImdbContext : DbContext
{
    public ImdbContext()
    {
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=IMDB_Project;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Name>(entity =>
        {
            entity.Property(e => e.NameId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nameID");
            entity.Property(e => e.BirthYear).HasColumnName("birthYear");
            entity.Property(e => e.DeathYear).HasColumnName("deathYear");
            entity.Property(e => e.PrimaryName)
                .HasMaxLength(125)
                .IsUnicode(false)
                .HasColumnName("primaryName");
            entity.Property(e => e.PrimaryProfession)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("primaryProfession");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.TitleId);

            entity.Property(e => e.TitleId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("titleID");
            entity.Property(e => e.AverageRating)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("averageRating");
            entity.Property(e => e.NumVotes).HasColumnName("numVotes");

            entity.HasOne(d => d.Title).WithOne(p => p.Rating)
                .HasForeignKey<Rating>(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Titles");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CE9D23A7A3");

            entity.Property(e => e.ReviewDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ReviewText).HasMaxLength(1000);
            entity.Property(e => e.ReviewerName).HasMaxLength(100);
            entity.Property(e => e.TitleId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Title).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Titles");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("titleID");
            entity.Property(e => e.EndYear).HasColumnName("endYear");
            entity.Property(e => e.IsAdult).HasColumnName("isAdult");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("originalTitle");
            entity.Property(e => e.PrimaryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("primaryTitle");
            entity.Property(e => e.RuntimeMinutes).HasColumnName("runtimeMinutes");
            entity.Property(e => e.StartYear).HasColumnName("startYear");
            entity.Property(e => e.TitleType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("titleType");

            entity.HasMany(d => d.Names).WithMany(p => p.Titles)
                .UsingEntity<Dictionary<string, object>>(
                    "Director",
                    r => r.HasOne<Name>().WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Directors_Names"),
                    l => l.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Directors_Titles1"),
                    j =>
                    {
                        j.HasKey("TitleId", "NameId");
                        j.ToTable("Directors");
                        j.IndexerProperty<string>("TitleId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("titleID");
                        j.IndexerProperty<string>("NameId")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("nameID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
