using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Xizzmat.Customer.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomerApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
