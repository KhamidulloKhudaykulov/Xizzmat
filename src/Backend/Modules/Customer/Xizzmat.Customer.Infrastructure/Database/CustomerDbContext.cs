using Microsoft.EntityFrameworkCore;
using Xizzmat.Customer.Domain.Entities;
using Xizzmat.Customer.Infrastructure.Extensions;

namespace Xizzmat.Customer.Infrastructure.Database;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options) { }

    public DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            
            entity.Property(e => e.Surname)
                  .IsRequired()
                  .HasMaxLength(100);
            
            entity.Property(e => e.PhoneNumber)
                  .IsRequired()
                  .HasMaxLength(20);
            
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("NOW()");
            
            entity.Property(e => e.IsDeleted)
                  .HasDefaultValue(false);

            entity.HasIndex(e => e.PhoneNumber)
                  .IsUnique()
                  .HasDatabaseName("IX_Customers_PhoneNumber_Unique");
        });
    }
}
