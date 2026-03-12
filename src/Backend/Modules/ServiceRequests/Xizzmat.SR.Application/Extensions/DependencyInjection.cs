using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Xizzmat.SR.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddSRApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
