using Microsoft.EntityFrameworkCore;
using Xizzmat.SR.Domain.Entities;
using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Infrastructure.Database;

public class ServiceRequestsDbContext : DbContext
{
    public ServiceRequestsDbContext(DbContextOptions<ServiceRequestsDbContext> options)
        : base(options) { }

    public DbSet<ServiceRequest> Workers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.ToTable("ServiceRequests");
            entity.HasKey(s => s.Id);

            entity.Property(w => w.CustomerId).IsRequired();
            entity.Property(w => w.WorkerId).IsRequired();
        });

        modelBuilder.Ignore<DomainEvent>();
    }
}
