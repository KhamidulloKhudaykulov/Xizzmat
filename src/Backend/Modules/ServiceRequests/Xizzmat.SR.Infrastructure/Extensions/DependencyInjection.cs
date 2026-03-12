using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xizzmat.SR.Application.Interfaces;
using Xizzmat.SR.Domain.Repositories;
using Xizzmat.SR.Infrastructure.Database;
using Xizzmat.SR.Infrastructure.Implementations;
using Xizzmat.SR.Infrastructure.Repositories;

namespace Xizzmat.SR.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddSRInfrastructure(
        this IServiceCollection services,
        IConfiguration cfg)
    {
        services.AddDbContext<ServiceRequestsDbContext>(options =>
        {
            options.UseNpgsql(cfg.GetConnectionString("ServiceRequestDb"));
        });

        services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEventBus, InMemoryEventBus>();

        return services;
    }
}
