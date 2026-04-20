using Axon.Application.Abstractions.Plugins;

namespace Axon.Host;

public sealed class Worker(ILogger<Worker> logger, IAxonPluginRegistry pluginRegistry) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var plugins = await pluginRegistry.GetPluginsAsync(stoppingToken);

        logger.LogInformation(
            "AXON host iniciado com {PluginCount} plugin(s): {Plugins}",
            plugins.Count,
            string.Join(", ", plugins.Select(plugin => plugin.Name)));

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("AXON host heartbeat em {Timestamp}", DateTimeOffset.UtcNow);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
