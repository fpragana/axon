using Axon.Core.Plugins;

namespace Axon.Application.Abstractions.Plugins;

public interface IAxonPluginRegistry
{
    Task<IReadOnlyCollection<IAxonPlugin>> GetPluginsAsync(CancellationToken cancellationToken = default);

    Task<IAxonPlugin?> FindByNameAsync(string pluginName, CancellationToken cancellationToken = default);

    Task<IAxonPlugin?> FindByActionAsync(string actionName, CancellationToken cancellationToken = default);
}
