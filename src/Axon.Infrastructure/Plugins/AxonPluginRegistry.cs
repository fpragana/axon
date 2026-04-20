using Axon.Application.Abstractions.Plugins;
using Axon.Core.Plugins;

namespace Axon.Infrastructure.Plugins;

public sealed class AxonPluginRegistry(IEnumerable<IAxonPlugin> plugins) : IAxonPluginRegistry
{
    private readonly IReadOnlyCollection<IAxonPlugin> _plugins = plugins.ToArray();

    public Task<IReadOnlyCollection<IAxonPlugin>> GetPluginsAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(_plugins);

    public Task<IAxonPlugin?> FindByNameAsync(string pluginName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pluginName);

        var plugin = _plugins.FirstOrDefault(plugin =>
            string.Equals(plugin.Name, pluginName, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(plugin);
    }

    public Task<IAxonPlugin?> FindByActionAsync(string actionName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(actionName);

        var plugin = _plugins.FirstOrDefault(plugin =>
            plugin.GetActions().Any(action => string.Equals(action.Name, actionName, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(plugin);
    }
}
