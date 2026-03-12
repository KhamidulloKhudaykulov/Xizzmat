using Xizzmat.Customer.Application.Extensions;
using Xizzmat.Customer.Infrastructure.Extensions;
using Xizzmat.Worker.Infrastructure.Extensions;
using Xizzmat.Worker.Application.Extensions;
using Xizzmat.SR.Application.Extensions;
using Xizzmat.SR.Infrastructure.Extensions;

namespace Xizzmat.Api.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddExternalServices(
        this IServiceCollection services,
        IConfiguration cfg)
    {
        services.AddCustomerInfrastructure(cfg);
        services.AddCustomerApplication();

        services.AddWorkerApplication();
        services.AddWorkerInfrastructure(cfg);

        services.AddSRApplication();
        services.AddSRInfrastructure(cfg);

        return services;
    }
}
