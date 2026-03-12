using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Xizzmat.Worker.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
