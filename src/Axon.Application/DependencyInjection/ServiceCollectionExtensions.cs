using Axon.Application.Abstractions.Execution;
using Axon.Application.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Axon.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAxonApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<ICommandExecutionEngine, CommandExecutionEngine>();
        return services;
    }
}
