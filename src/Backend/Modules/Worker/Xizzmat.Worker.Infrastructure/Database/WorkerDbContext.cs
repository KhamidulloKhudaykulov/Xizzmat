using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Primitives;
using Xizzmat.Worker.Infrastructure.Extensions;

namespace Xizzmat.Worker.Infrastructure.Database;

public class WorkerDbContext : DbContext
{
    public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
        : base(options) { }

    public DbSet<WorkerEntity> Workers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Worker aggregate
        modelBuilder.Entity<WorkerEntity>(entity =>
        {
            entity.ToTable("Workers");
            entity.HasKey(w => w.Id);

            entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
            entity.Property(w => w.Surname).IsRequired().HasMaxLength(100);
            entity.Property(w => w.Phone).IsRequired().HasMaxLength(50);
            entity.Property(w => w.Email).HasMaxLength(200);
            entity.Property(w => w.City).HasMaxLength(100);
            entity.Property(w => w.Rating).IsRequired();
            entity.Property(w => w.IsActive).IsRequired();
            entity.Property(w => w.IsDeleted).IsRequired();
            entity.Property(w => w.CreatedAt).IsRequired();

            entity.HasMany(w => w.Locations)
                  .WithOne(l => l.Worker)
                  .HasForeignKey(l => l.WorkerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(w => w.Skills)
                  .WithOne(s => s.Worker)
                  .HasForeignKey(s => s.WorkerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(w => w.Services)
                  .WithOne(s => s.Worker)
                  .HasForeignKey(s => s.WorkerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(w => w.Reviews)
                  .WithOne(r => r.Worker)
                  .HasForeignKey(r => r.WorkerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkerLocation>(entity =>
        {
            entity.ToTable("WorkerLocations");
            entity.HasKey(l => l.Id);
            entity.Property(l => l.City).IsRequired().HasMaxLength(100);
            entity.Property(l => l.District).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<WorkerSkill>(entity =>
        {
            entity.ToTable("WorkerSkills");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<WorkerService>(entity =>
        {
            entity.ToTable("WorkerServices");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(150);
            entity.Property(s => s.Price).HasColumnType("decimal(18,3)");
        });

        modelBuilder.Entity<WorkerReview>(entity =>
        {
            entity.ToTable("WorkerReviews");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Comment).HasMaxLength(1000);
            entity.Property(r => r.CreatedAt).IsRequired();
        });

        modelBuilder.Ignore<DomainEvent>();
    }
}
