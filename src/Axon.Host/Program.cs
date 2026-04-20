using Axon.AI.DependencyInjection;
using Axon.Application.DependencyInjection;
using Axon.Core.Plugins;
using Axon.Infrastructure.DependencyInjection;
using Axon.Plugins.HomeAssistant;
using Axon.Plugins.SystemActions;

namespace Axon.Host;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

        builder.Services.AddAxonApplication();
        builder.Services.AddAxonAi();
        builder.Services.AddAxonInfrastructure();
        builder.Services.AddSingleton<IAxonPlugin, SystemActionsPlugin>();
        builder.Services.AddSingleton<IAxonPlugin, HomeAssistantPlugin>();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}
