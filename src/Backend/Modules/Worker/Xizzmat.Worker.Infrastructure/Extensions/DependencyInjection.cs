using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xizzmat.Worker.Application.Interfaces;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Infrastructure.Database;
using Xizzmat.Worker.Infrastructure.Implementations;
using Xizzmat.Worker.Infrastructure.Repositories;

namespace Xizzmat.Worker.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerInfrastructure(
        this IServiceCollection services,
        IConfiguration cfg)
    {
        services.AddDbContext<WorkerDbContext>(options =>
        {
            options.UseNpgsql(cfg.GetConnectionString("WorkerDb"));
        });

        services.AddScoped<IWorkerRepository, WorkerRepository>();
        services.AddScoped<IWorkerSkillRepository, WorkerSkillRepository>();
        services.AddScoped<IWorkerLocationRepository, WorkerLocationRepository>();
        services.AddScoped<IWorkerServiceRepository, WorkerServiceRepository>();
        services.AddScoped<IWorkerReviewRepository, WorkerReviewRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IWorkerService, WorkerService>();

        return services;
    }
}
