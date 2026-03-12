using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xizzmat.Customer.Application.Interfaces;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Infrastructure.Database;
using Xizzmat.Customer.Infrastructure.Implementations;
using Xizzmat.Customer.Infrastructure.Repositories;

namespace Xizzmat.Customer.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomerInfrastructure(
        this IServiceCollection services,
        IConfiguration cfg)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseNpgsql(cfg.GetConnectionString("CustomerDb"));
        });

        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
