namespace Axon.Core.Plugins;

public sealed record PluginExecutionRequest(
    string PluginName,
    string ActionName,
    IReadOnlyDictionary<string, object?> Parameters);
