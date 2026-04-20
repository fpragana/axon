using Axon.Core.Plugins;

namespace Axon.Plugins.HomeAssistant;

public sealed class HomeAssistantPlugin : IAxonPlugin
{
    public string Name => "HomeAssistant";

    public string Description => "Plugin reservado para integração futura com Home Assistant.";

    public IReadOnlyCollection<PluginActionDescriptor> GetActions() => Array.Empty<PluginActionDescriptor>();

    public Task<ExecutionResult> ExecuteAsync(
        string actionName,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ExecutionResult.Failure(
            "O plugin HomeAssistant ainda não foi implementado nesta fase inicial.",
            $"Action: {actionName}"));
    }
}
