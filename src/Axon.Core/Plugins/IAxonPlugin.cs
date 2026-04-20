namespace Axon.Core.Plugins;

public interface IAxonPlugin
{
    string Name { get; }

    string Description { get; }

    IReadOnlyCollection<PluginActionDescriptor> GetActions();

    Task<ExecutionResult> ExecuteAsync(
        string actionName,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken = default);
}
