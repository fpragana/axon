namespace Axon.Core.Plugins;

public sealed record PluginActionDescriptor(
    string Name,
    string Description,
    IReadOnlyCollection<PluginParameterDescriptor> Parameters);
