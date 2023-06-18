using Microsoft.EntityFrameworkCore;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Persistence.Context;

public partial class TestDbContext : DbContext, ITestDbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; } = null!;

    public virtual DbSet<Question> Questions { get; set; } = null!;

    public virtual DbSet<Test> Tests { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => new { e.AnswerId, e.QuestionId, e.TestId }).HasName("PK_Answers_1");

            entity.Property(e => e.AnswerId).ValueGeneratedOnAdd();
            entity.Property(e => e.Text).HasMaxLength(200);

            entity.HasOne(d => d.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => new { d.QuestionId, d.TestId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Answers_Questions");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => new { e.QuestionId, e.TestId }).HasName("PK_Questions_1");

            entity.Property(e => e.QuestionId).ValueGeneratedOnAdd();
            entity.Property(e => e.Text).HasMaxLength(100);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Questions_Tests");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Tests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Tests_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Login, "IX_Users").IsUnique();

            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsUnicode(false);
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
