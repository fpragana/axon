using Axon.AI.Interpretation;
using Axon.Application.Abstractions.Intelligence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Axon.AI.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAxonAi(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IIntentInterpreter, MockIntentInterpreter>();
        return services;
    }
}
