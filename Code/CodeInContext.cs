using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Code.Models;
namespace Code;

public partial class CodeInContext : DbContext
{
    public CodeInContext()
    {
    }

    public CodeInContext(DbContextOptions<CodeInContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Codebase> Codebases { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ProblemSolving> ProblemSolvings { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CodeIN;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Codebase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Codebase__3214EC07846A455D");

            entity.ToTable("Codebase");

            entity.HasIndex(e => e.Name, "UQ__Codebase__737584F667128505").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC074DD2CA2F");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534BC813957").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
