namespace Axon.Core.Plugins;

public sealed record PluginParameterDescriptor(
    string Name,
    string Type,
    string Description,
    bool IsRequired);
